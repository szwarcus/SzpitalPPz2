$(function () {
    $("#multiSelectDropDown").chosen({
        width: "50%",
        placeholder_text_multiple: "Wybierz leki"
    });
    $("#singleSelectDropDown").chosen({
        width: "50%",
        placeholder_text_multiple: "Wybierz leki"
    });

    $('#historicVisits > div').click(function () {
        $('.bg-modal').css("display", "flex");

        var data = $(this).text();

        var date = data.replace(/ +(?= )/g, '').split(" ");
        var patientId = $('#patientId').val();
        $.getJSON('/Doctor/Home/VisitDetails', { date: date[1] + " " + date[2], patientId: patientId }, (visit) => {

            if (visit === null) {
                return;
            }
            $('#DescriptionContent').text(visit.visitDescription);
            $('#MedicamentsContent').text(visit.medicaments);
            $('#VisitDate').text("Szczegóły wizyty z dnia " + date[1] + " " + date[2]);
            $('#DescriptionPrescriptionContent').text(visit.dosage);
        });
       


    });
    $('.closeX').click(function () {
        $('.bg-modal').css("display", "none");
    });
});
