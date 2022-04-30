
function sayWelcomeToUser(user) {
    $(document).ready(function () {
        const alertCheck = localStorage.getItem('alert')
        if (alertCheck === null) {
            $('body').append(`    <div class="welcome-alert active">
    <h4>Hoşgeldiniz Sayın ${user}</h4>
    <p> İhtiyaç duyduğunuz ürünü bulamamanız halinde destek ekibimizle irtibata geçiniz. Liste oluştururken sizlere
        kolaylıklar dileriz.</p>
        <a onclick="removeAlert()" class="btn btn-success btn-sm">Anladım</a>
        </div>`)
            setTimeout(() => {
                $('.welcome-alert').css('top', '50%')
            }, 400)

            $('#main-container').addClass('passive')

        }
    })
}

const dialogFadeIn = () => {
    setTimeout(() => {
        $('#dialog').css('margin-top', '70px')
    })
}

const dialogFadeOut = () => {
    setTimeout(() => {
        $('#dialog').css('margin-top', '-100%')
    })
}

const removeAlert = () => {
    $('.welcome-alert').remove()
    $('#main-container').removeClass('passive')
    localStorage.setItem('alert', 'true')
}

//OPEN DIALOG
const button = document.querySelector('#dialog-button')
const modal = document.querySelector('#dialog')
const buttonClose = document.querySelector('#close-button')

//basic
button.addEventListener('click', () => {
    $('#list-id').show()
    $('#list-title').text('Liste Oluştur')
    getProducts('/Home/GetProducts')
    dialogFadeIn()
    modal.showModal()

})

const closeModal = () => {
    $('#dialog').attr('data-list-id', '0')
    dialogFadeOut()
    modal.close()
}

//update
let buttons = document.querySelectorAll('.btnUpdateList')
buttons.forEach((button) => {
    button.addEventListener('click', () => {
        const cartId = button.getAttribute("data-cartId")
        const title = button.getAttribute("data-title")
        $('#dialog').attr('data-list-id', cartId)
        $('#list-id').hide()
        $('#list-title').text(title)
        getProducts('/Home/GetProductsNotInList?cartId=' + cartId)
        dialogFadeIn()
        modal.showModal()
    })
})
buttonClose.addEventListener('click', closeModal)

//GET PRODUCTS
function getProducts(url) {
    $('#product-list > tbody').empty()
    $.get(url, function (data) {
        data.forEach(function (item) {
            $('#product-list > tbody').each(function () {
                $(this).append(`<tr>
                <td>
                    <input data-id="${item.id}" type="checkbox" class="product-check" name="checked" id="${item.name}" />
                    <label class="checkbox" for="${item.name}">Seç</label>
                </td>
                <td>
                    <img  width="50" height="50" src="${item.photo}" alt="${item.name}" />
                </td>
                <td>${item.category.name}</td>
                <td>
                ${item.name}
                </td>
                <td>
                    <textarea  class="form-control" name="not" id="not-${item.id}" cols="8" rows="3"></textarea>
                </td>
            </tr>`)
            })
        })

    }).done().fail((error) => console.log(error))
}

//SEARCH SECTION
$('#search-form').hide()
$('#search-button').click(function () {
    if ($('#search-button >i').hasClass('fa-search')) {
        $('#search-button >i').removeClass('fa-search')
        $('#search-button >i').addClass('fa-times')
    }
    else {
        $('#search-button >i').removeClass('fa-times')
        $('#search-button >i').addClass('fa-search')
    }
    $('#search-form').fadeToggle()
})


$('#product-name-search').on('keyup', function () {
    let value = $(this).val().toLowerCase()
    $('#product-list > tbody> tr').filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    })
})

$('#category-list').on('change', function () {
    let value = $(this).val()
    if (value == 0) {
        $('#product-list > tbody>tr').show()
        return
    }
    $('#product-list > tbody> tr').filter(function () {
        let category = $(this).find('td:nth-child(3)').text()
        $(this).toggle(category === value)
    })
})


//#region SAVE SECTION
let cartItems = []
$('#btnSave').click(() => {
    if ($('#dialog').attr('data-list-id') != 0) {
        const cartId = $('#dialog').attr('data-list-id')
        console.log(cartId)
        $('input[name="checked"]').each((index, element) => {
            if (element.checked == true) {
                const note = $(`#not-${element.getAttribute("data-id")}`).val()
                cartItems.push({
                    shoppingCartId: cartId,
                    productId: element.getAttribute("data-id"),
                    note: note,
                    status: true
                })
            }
        })
        $.post('/ShoppingCart/AddListWithOutTitle', {
            shoppingCartItemAddDtos: cartItems
        }).done(() => {
            window.location.reload()
        })
    }
    if ($('#dialog').attr('data-list-id') == 0) {
        const title = $('#list-id').val()
        if (title === '') {
            alert('Title Boş Olamaz')
            return;
        }  
        $('input[name="checked"]').each((index, element) => {
            if (element.checked == true) {
                const note = $(`#not-${element.getAttribute("data-id")}`).val()
                cartItems.push({
                    productId: element.getAttribute("data-id"),
                    note: note,
                    status: true
                })
            }
        })
        $.post('/ShoppingCart/AddList', {
            title: title,
            shoppingCartItemAddDtos: cartItems
        }).done(() => {
            window.location.reload()
        })
    }
    /*dialogFadeOut()*/
})
//#endregion



