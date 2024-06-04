const loadShoppingBagPage = () => {
    const bagItems = sessionStorage.getItem("productArray");
    const jsonBagItems = JSON.parse(bagItems);
    console.log(jsonBagItems);
    showBagItems(jsonBagItems);
}

const showBagItems = (bagItems) => {
    let sum = 0;
    console.log(bagItems);
    const template = document.getElementById("temp-row").content;
    const tbody = document.querySelector("tbody");
    tbody.replaceChildren();
    const prodId = [];
    bagItems.forEach(item => {
        if (prodId.includes(item.id)) {
            const prod = document.getElementById(item.id);
            const quantityDiv = prod.querySelector('.quantity').innerHTML++;
            console.log(quantityDiv);
        }
        else {
            const clone = template.cloneNode(true);
            const image = clone.querySelector(".image");
            const itemName = clone.querySelector(".itemName");
            const itemNumber = clone.querySelector(".itemNumber");
            clone.querySelector("tr").id = item.id;
            prodId.push(item.id);

            image.src = "./Images/" + item.prodImage;
            itemName.innerText = item.prodName;
            itemNumber.innerText += " " + item.price;
            tbody.appendChild(clone);
        } 
        sum += item.price;
    })
    document.getElementById("itemCount").innerHTML = bagItems.length;
    document.getElementById("totalAmount").innerHTML = sum ;
}

const deleteProd = () => {    
    const deleteButton = event.target;
    const row = deleteButton.closest('.item-row'); 
    const rowId = row.id;
    const itemsBag = JSON.parse(sessionStorage.getItem("productArray"));
    const prod = itemsBag.filter(prod => prod.id !== parseInt(rowId));
    sessionStorage.setItem("productArray", JSON.stringify(prod));
    showBagItems(prod);    
}

const placeOrder = () => {
    if (!(sessionStorage.getItem("user")))
    {
        window.location.href = "login.html";
    }
    else
        createOrder();
}

const createOrder = async () => {
    const items = JSON.parse(sessionStorage.getItem("productArray"));
    const userId = JSON.parse(sessionStorage.getItem("user")).id;
    const orderSum = parseInt(document.getElementById("totalAmount").innerHTML);
    const orderDate = new Date();
    const orderItems = [];
    items.forEach(item => {
        let prod = document.getElementById(item.id);
        let quantity = prod.querySelector('.quantity').innerHTML;
        let productId = item.id;
        let orderItem = { productId, quantity };

        orderItems.push(orderItem);
    })
    console.log(orderItems);
    const order = { userId, orderDate, orderSum, orderItems };
    try {
        const res = await fetch('/api/orders',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(order)
            });

        if (!res.ok)
            throw new Error("Error place order")
        const created = await res.json();
        alert(`Order num: ${created.id} has been successfully ordered And OrderSum: ${orderSum}`);
        const tbody = document.querySelector("tbody");
        tbody.replaceChildren();
        sessionStorage.setItem("productArray", []);
        document.getElementById("itemCount").innerHTML = "";
        document.getElementById("totalAmount").innerHTML = "";
    } catch (ex) {
        alert(ex.message);
    }
}
