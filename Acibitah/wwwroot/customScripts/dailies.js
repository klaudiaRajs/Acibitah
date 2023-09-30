function check(element, taskId) {
    if (!element.checked) {
        return; 
    }
    $.ajax({
        type: "GET", 
        url: "/Task/CheckDaily",
        data: {id: taskId}, 
        success: function (result) {
            location.reload();
        },
        error: function (err) {
            alert("failure"); 
        }
    });
}