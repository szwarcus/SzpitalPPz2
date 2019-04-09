
$(".upComingVisits").click(function () {
    var href = $(this).attr('href');
    window.location.href = href;
});
$(".NextPreviousWeek").click(function () {
    var href = $(this).attr('href');
    window.location.href = href;
});

$('#datePicker').datepicker({
    showOn: "button",
    buttonImage: "/images/calendar.svg",
    buttonImageOnly: true,
    
    dateFormat: "dd-mm-yy",
    onClose: function (dateText, inst) {
        if (dateText.length > 1)
            window.location.href = "/Doctor/Home/TimeTable?dateTime=" + dateText;
    }
});

