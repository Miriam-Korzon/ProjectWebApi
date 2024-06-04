const loadProductPage = async () => {
    const categories = await getCategories();
    showCategories(categories);
    const products = await getProducts("");
    showProducts(products);
    const product = sessionStorage.getItem("productArray");
    const productJson = JSON.parse(product);
    document.getElementById("ItemsCountText").innerHTML = productJson.length;
    cart = productJson;
}

const filterProducts = async () => {
    const checkboxArray = document.getElementsByClassName("opt");
    const desc = document.getElementById("nameSearch").value;
    const minPrice = document.getElementById("minPrice").value;
    const maxPrice = document.getElementById("maxPrice").value;
    let url = "?desc=" + desc + "&minPrice=" + minPrice + "&maxPrice=" + maxPrice;

    for (let i = 0; i < checkboxArray.length; i++) {
        if (checkboxArray[i].checked) {
            url += "&categoryIds=" + checkboxArray[i].id;
        }
    } 
    const products = await getProducts(url);
    showProducts(products);
}

const getCategories = async () => {
    try {
        const res = await fetch(`/api/categories`,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

        if (!res.ok)
            throw new Error("No Categories Found")
        const categories = await res.json();
        return categories;
    }
    catch (ex) {
        alert(ex.message)
    }
}

const showCategories = (categories) => {
    const template = document.getElementById("temp-category").content;
    const categoryList = document.getElementById("categoryList");
    categories.forEach(category => {
        const clone = template.cloneNode(true);
        const checkbox = clone.querySelector(".opt");
        const label = clone.querySelector("label");
        const optionName = clone.querySelector(".OptionName");
        

        checkbox.id = category.id;
        checkbox.value = category.id;
        label.setAttribute("for", category.id);
        optionName.textContent = category.categoryName;
        categoryList.appendChild(clone);
    });    
} 

const getProducts = async (url) => {
    try {
        const res = await fetch(`/api/products` + url,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

        if (!res.ok)
            throw new Error("No Products Found")
        const products = await res.json();
        return products;
    }
    catch (ex) {
        alert(ex.message)
    }
}

const showProducts = (products) => {
    let min = 1000;
    let max = 0;
    const template = document.getElementById("temp-card").content;
    const PoductList = document.getElementById("PoductList");
    PoductList.replaceChildren();
    console.log(products);
    products.forEach(product => {
        const clone = template.cloneNode(true);
        const image = clone.querySelector("img");
        const h1 = clone.querySelector("h1");
        const price = clone.querySelector(".price");
        const description = clone.querySelector(".description");
        const button = clone.querySelector("button");

        image.src = "./Images/" + product.prodImage;
        h1.textContent = product.prodName;
        price.textContent = product.price + " ₪";
        description.textContent = product.prodDescription;
        button.addEventListener('click', () => { addToCart(product) });
        if (product.price > max)
            max = product.price;
        if (product.price < min)
            min = product.price;
        PoductList.appendChild(clone);
    });
    const counter = document.getElementById("counter");
    counter.innerHTML = products.length;
    const minPrice = document.getElementById("minPrice");
    minPrice.placeholder = min;
    const maxPrice = document.getElementById("maxPrice");
    maxPrice.placeholder = max;
} 
let cart = [];
const addToCart = (product) => {
    console.log(product);
    document.getElementById("ItemsCountText").innerHTML++;
    cart.push(product);
    sessionStorage.setItem("productArray", JSON.stringify(cart));
}