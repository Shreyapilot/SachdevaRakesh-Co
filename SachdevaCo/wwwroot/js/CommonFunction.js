function showAlertMessage(message, Name, typeNotification) {
    $.bootstrapGrowl(message, // Messages
        { // options
            type: typeNotification, //"info", // info, success, warning and danger
            ele: Name, //"body", // parent container
            offset: {
                from: "top",
                amount: 20
            },
            align: "right", // right, left or center
            width: 400,
            delay: 5000,
            allow_dismiss: true, // add a close button to the message
            stackup_spacing: 10
        });
}

