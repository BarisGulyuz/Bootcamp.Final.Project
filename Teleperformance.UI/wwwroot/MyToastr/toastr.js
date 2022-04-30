const showToastr = (type,  message, ms=4000) => {
    if(!message) message  = 'No message u provided'
    const toastrIndex = $('.toastr-container').find('.bg-toastr').length

    const id = Math.floor(Math.random() * 999999999)

    $('.toastr-container').append(CreateToastr(type, id, message));
    
    const toastrElement = $(`#toast-${id}`)

    const toastrTop = toastrElement.position().top
    toastrElement.css('top', (toastrTop + toastrIndex * 100))
    if($(window).width() < 768)
    {
        toastrElement.css('top', (toastrTop + toastrIndex * 65))
    }
    toastrElement.hide()
    toastrElement.fadeIn(1000).fadeOut(ms, () => {
        toastrElement.remove()
    })
}

const CreateToastr = (type, id, message) => {
    let icon = ''
    switch (type) {
        case 'success':
            icon = 'fas fa-check'
            break;
        case 'error':
            icon = 'fas fa-times'
            break;
        case 'warning':
            icon = 'fas fa-exclamation'
            break;
        case 'info':
            icon = 'fas fa-info'
            break;
        default:
            icon = 'fas fa-info'
            break;
    }
    return ` <div class="bg-toastr ${type}" id="toast-${id}">
                <a  onclick="closeToastr(${id})"> <i class="fas fa-times-circle"></i> </a>
                <i class="${icon}"></i>
                <div>
                    <span>${type.toUpperCase()}</span>
                    <span>${message}</span>
                </div>
            </div>`
}

const closeToastr = (id) => {
    $(`#toast-${id}`).remove()
}





