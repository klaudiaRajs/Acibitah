function check(element, taskId, url) {
    if (!element.checked) {
        return; 
    }
    $.ajax({
        type: "GET", 
        url: url,
        data: {id: taskId}, 
        success: function (result) {
            location.reload();
        },
        error: function (err) {
            alert("failure"); 
        }
    });
}