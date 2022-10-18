const cookieName="cart-items";
function addToCart(id, name, price, picture) {
    debugger;
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products)

    }
    const count = Number($("#product_count").val());
    const currentProduct = products.find(x => x.Id == id);
    if (currentProduct !== undefined) {
        products.find(x => x.Id == id).Count += count;
    } else {

        const product = { "Id": id, "Name": name, "Price": price, "Picture":picture,"Count": count }
        products.push(product);
    }

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart()
}


function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    console.log(products)
    $("#cart_items_count").text(products.length)

    let cart_items_warraper = $("#cart-items-wrapper");
    cart_items_warraper.html('');

    products.forEach(x => {
        let product = `<div class="single-cart-item">
                                                    <a href="javascript:void(0)" onclick="RemoveFromCart('${x.Id}')" class="remove-icon">
                                                        <i class="ion-android-close"></i>
                                                    </a>
                                                    <div class="image">
                                                        <a href="single-product.html">
                                                            <img src="/Files/${x.Picture}"
                                                                 class="img-fluid" alt="">
                                                        </a>
                                                    </div>
                                                    <div class="content">
                                                        <p class="product-title">
                                                            <a href="single-product.html">${x.Name}</a>
                                                        </p>
                                                        <p class="count">مبلغ:${x.Price}</p>
<p class="count">تعداد :${x.Count}</p>
                                                    </div>
                                                </div>`;

        cart_items_warraper.append(product);


    })
}


function RemoveFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    var index = products.findIndex(x => x.Id == id)
    products.splice(index, 1)
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart()
}


function ChangeCartItemCount(id, count) {

    debugger;

    let products = $.cookie(cookieName);
    products = JSON.parse(products);

    const currentProduct = products.find(x => x.Id == id);
    currentProduct.Count = count;
    const newTotalPrice = parseInt(currentProduct.price) * parseInt(count);

    $("#total-count-product-" + id).text(newTotalPrice);

    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart()
}
