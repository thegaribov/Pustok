
let productQuantitiesIncrease = document.querySelectorAll(".inc")
let productQuantitiesDecrease = document.querySelectorAll(".dec")

productQuantitiesIncrease.forEach(pq => {
    pq.addEventListener("click", (e) => {
        updateProductAmount("https://localhost:7258/basket/increase-basket-product", e);
    })
})

productQuantitiesDecrease.forEach(pq => {
    pq.addEventListener("click", (e) => {
        updateProductAmount("https://localhost:7258/basket/decrease-basket-product", e);
    })
})


function updateProductAmount(updateUrl, e) {
    let productDetailsElement = $(e.target).closest(".product-details");
    let productQuantityElement = productDetailsElement.find(".product-quantity");
    let productAmount = productDetailsElement.find(".product-subtotal").find(".amount");
    let basketProductId = productQuantityElement.data("basket-product-id");

    let url = `${updateUrl}/${basketProductId}`;

    $.ajax(url)
        .done(function (data, _, response) {
            if (response.status == 200) {
                productAmount.html(`$${data.total}`);
            }
            else if (response.status == 204) {
                productDetailsElement.remove();
            }
        })
        .fail(function () {
            alert("error");
        });
}