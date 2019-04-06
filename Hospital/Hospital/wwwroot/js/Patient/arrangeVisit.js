var doctorsWithAvailableVisits = [];

$('#specializationName, #visitDay').on('change', () => {
    var selectedSpecializationName = $('#specializationName').val();
    var selectedVisitDay = $('#visitDay').val();

    if (selectedVisitDay === null || selectedSpecializationName === null
        || selectedVisitDay === "" || selectedSpecializationName === "") {

        $('#doctorId').attr('disabled', true);
        $('#visitHour').attr('disabled', true);
        resetDoctorsWithAvailableVisits();
        return;
    }

    $.getJSON('/Patient/Home/Doctors', { day: selectedVisitDay, specializationName: selectedSpecializationName }, (doctors) => {
        if (doctors === null || doctors.length < 1) {
            return;
        }

        resetDoctorsWithAvailableVisits();

        doctors.forEach((doctor) => {
            var doctorWithAvailableVisits = {};

            doctorWithAvailableVisits['DoctorId'] = doctor.doctorId;
            doctorWithAvailableVisits['Name'] = doctor.firstName + ' ' + doctor.lastName;
            doctorWithAvailableVisits['AvailableVisitsHours'] = doctor.availableVisits;

            doctorsWithAvailableVisits.push(doctorWithAvailableVisits);

            var option = new Option(doctorWithAvailableVisits['Name'], doctorWithAvailableVisits['DoctorId']);
            $(option).html(doctorWithAvailableVisits['Name']);
            $('#doctorId').append(option);
        });

        setAvailableHours(0);

        $('#doctorId').removeAttr('disabled');
        $('#visitHour').removeAttr('disabled');
    });
});

$('#doctorId').on('change', () => {
    var selectedDoctorId = $('#doctorId').val();

    var index = doctorsWithAvailableVisits.findIndex((doctorWithAvailableVisits) => {
        return doctorWithAvailableVisits['DoctorId'] === selectedDoctorId;
    });

    setAvailableHours(index);
});

var setAvailableHours = (indexDoctorsWithAvailableVisits) => {
    if (indexDoctorsWithAvailableVisits < 0 || indexDoctorsWithAvailableVisits >= doctorsWithAvailableVisits.length) {
        return;
    }

    $('#visitHour').empty();

    doctorsWithAvailableVisits[indexDoctorsWithAvailableVisits]['AvailableVisitsHours'].forEach((availableVisit) => {
        var option = new Option(availableVisit, availableVisit);
        $(option).html(availableVisit);
        $('#visitHour').append(option);
    });
};

var resetDoctorsWithAvailableVisits = () => {
    $('#doctorId').empty();
    $('#visitHour').empty();
    doctorsWithAvailableVisits = [];
};

$("#visitDay").datepicker({
    dateFormat: "yy-mm-dd",
    minDate: 0,
    maxDate: "+1M"
});