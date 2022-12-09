



$(document).ready(function () {
    $('#paymentTable').DataTable({
        ajax: {
            url: 'https://localhost:7095/api/transaction',
            dataSrc: 'Data'
        },
        columns: [
            {
                data: "Order.Occupant.Name",
                render: (data, type, row, meta) => {
                    return data;
                }
            },
            {
                data: 'Amount',
                render: (data) => {
                    return data;
                }
            },

            {
                data: null,
                "render": function (data, type, row, meta) {
                    return `<img src="${data.ProofPayment}" width="350" >`;
                }
            },
            {
                data: "Order.EntryDate",
                render: (data) => {
                    return new Date(data).toLocaleDateString('id-ID', {
                        weekday: 'long',
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                    });
                }
            },
            {
                data: "Order.OutDate",
                render: (data) => {
                    return new Date(data).toLocaleDateString('id-ID', {
                        weekday: 'long',
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                    });
                }
            },
            {
                data: 'PaymentDate',
                render: (data) => {
                    return new Date(data).toLocaleDateString('id-ID', {
                        weekday: 'long',
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                    });
                }
            },
            {
                data: 'Status',
                render: (data) => {
                    return data ? '<span class="badge badge-pill badge-success"><i class="fa fa-check mr-5"></i>Approved!</span>' : '<span class="badge badge-pill badge-info">On Hold</span>';
                }
            },
            {
                data: 'PaymentId',
                render: (data, type, row, meat) => {

                    return row.Status ? `<button type="button" class="btn btn-alt-primary mr-5 mb-5" disabled><i class="fa fa-thumbs-up mr-5"></i>Valid</button>` : `<button type="button" class="btn btn-alt-primary mr-5 mb-5"   onclick="approvePayment(${data})"><i class="fa fa-thumbs-up mr-5"></i>Valid</button> <button type="button" class="btn btn-alt-danger mr-5 mb-5" onclick="rejectPayment(${data})"><i class="fa fa-times mr-5"></i>Reject</button>`

                }
            }

        ],
        dom: 'Bfrtip',
        buttons: {
            buttons: [
                {
                    extend: 'copy',
                    className: 'btn btn-secondary',
                    exportOptions: {
                        columns: [0, 1]
                    }
                },
                { extend: 'colvis', className: 'btn btn-secondary' },
                {
                    extend: 'pdf',
                    className: 'btn btn-secondary',
                    exportOptions: {
                        columns: [0, 1]
                    }
                },
                {
                    extend: 'excel',
                    className: 'btn btn-secondary',
                    exportOptions: {
                        columns: [0, 1]
                    }
                }
            ],
            dom: {
                button: {
                    className: 'btn'
                }
            }
        }

    }

    )

    //table.buttons().container().addClass("mb-3 me-3")

})





//form
console.log('ayooo')
const inputEntryDateEl = document.getElementById('entryDate')
const inputRentPeriodEl = document.getElementById('rentPeriod')

inputRentPeriodEl.addEventListener('change', calculateOutDate)

inputEntryDateEl.addEventListener('change', calculateOutDate)

function calculateOutDate() {

    dateEntryDateValue = new Date(inputEntryDateEl.value);

    let getMonthEntryDate = dateEntryDateValue.getMonth()
    let getYearEntryDate = dateEntryDateValue.getFullYear()
    let getDateEntryDate = dateEntryDateValue.getDate() /*=== 31 ? 30 : dateEntryDateValue.getDate() */



    let rentPeriod = document.getElementById('rentPeriod').value;



    let result = new Date(getYearEntryDate, (getMonthEntryDate + +rentPeriod), getDateEntryDate)


    let resultMonth = `0${result.getMonth() + 1}`.slice(-2);
    let resultDate = `0${result.getDate()}`.slice(-2);


    let resultString = `${result.getFullYear()}-${resultMonth}-${resultDate}`

    document.getElementById('outDate').value = resultString


    //calculate amount when change rentperiod 
    const totalAmountElement = document.getElementById('totalAmount')



    const roomPrice = document.getElementById('totalAmount').getAttribute('data-room-price')

    totalAmountElement.value = +roomPrice * +rentPeriod


}


$.ajax({
    url: 'https://localhost:7095/api/Room',
    type: 'GET',
    async: false,
    success: function (res) {


        let temp = `<option value="" disabled selected>pilih kamar</option>`
        res.Data.forEach(room => {

            console.log(room.Status == true && room.PaymentId)

            if (room.Status == true && room.PaymentId != null) {
                return
            }


            $.ajax({
                url: `https://localhost:7095/api/RoomPrice/${room.RoomPriceId}`,
                type: 'GET',
                async: false,
                success: function (res) {

                    console.log

                    temp += `<option data-room-price-id="${room.RoomPriceId}" value="${room.Id}">Kamar no ${room.Id} (Tipe Kamar ${res.Data.RoomType})</option>`



                }
            })





        })

    

        document.getElementById('selectedRoom').innerHTML = temp

    }
})



//event ketika pilih dropdown kamar
let amountTotalFormGroup = document.getElementById('totalAmount').closest('.form-group, .row')
amountTotalFormGroup.style.display = 'none'

document.getElementById('selectedRoom').addEventListener('change', calculateTotalAmount)

function calculateTotalAmount(event) {

    const roomPriceId = $('#selectedRoom').find(':selected').data('room-price-id')
    const rentPeriod = document.getElementById('rentPeriod').value;
    $.ajax({
        url: `https://localhost:7095/api/RoomPrice/${roomPriceId}`,
        type: 'GET',
        success: function (res) {
            console.log(res)
            const roomPrice = res.Data.Price

            document.getElementById('totalAmount').setAttribute('data-room-price', roomPrice)
            document.getElementById('totalAmount').value = +roomPrice * +rentPeriod
            amountTotalFormGroup.style.display = 'block'
        }
    })


}


//submit form
document.getElementById('transactionForm').addEventListener('submit', sendNewTransaction)



function sendNewTransaction(event) {

    event.preventDefault()

    const formElement = event.target


    if (!formElement.checkValidity()) {
        event.stopPropagation()
        formElement.classList.add('was-validated')
        return
    }


    //const formEl = document.querySelector('#transactionForm');
    const formData = new FormData();




    const NIKValue = document.querySelector('#occupantNIK').value
    const nameValue = document.querySelector('#occupantName').value

    let genderValue = null

    let radios = document.getElementsByName('Gender').values()
    for (let input of radios) {
        if (input.checked) {
            genderValue = input.value
        }
    }

    const contactValue = document.querySelector('#occupantContact').value
    const cityValue = document.querySelector('#occupantCity').value
    const religionValue = document.querySelector('#occupantReligion').value
    const birthDateValue = document.querySelector('#occupantBirthDate').value
    const emailValue = document.querySelector('#userEmail').value
    const passwordValue = document.querySelector('#userPassword').value
    const roomValue = document.querySelector('#selectedRoom').value
    const entryDateValue = document.querySelector('#entryDate').value

    const outDateValue = document.querySelector('#outDate').value
    const amountValue = +document.querySelector('#totalAmount').value
    const paymentDateValue = document.querySelector('#paymentDate').value
    const imageFile = document.querySelector('#Image').files[0]


    formData.append('NIK', NIKValue)
    formData.append('Name', nameValue)
    formData.append('Gender', genderValue)
    formData.append('Contact', contactValue)
    formData.append('City', cityValue)
    formData.append('Religion', religionValue)
    formData.append('BirthDate', birthDateValue)
    formData.append('Email', emailValue)
    formData.append('Password', passwordValue)
    formData.append('RoomId', roomValue)
    formData.append('EntryDate', entryDateValue)
    formData.append('PaymentDate', paymentDateValue)
    formData.append('Amount', amountValue);
    formData.append('OutDate', outDateValue);
    formData.append('Image', imageFile);


    for (let a of formData.values()) {
        console.log(a)
    }

    //const data = {
    //    Email: formData.get('Email'),
    //    Password: formData.get('Password'),
    //    NIK: formData.get('NIK'),
    //    Name: formData.get('Name').toLowerCase(),
    //    Gender: formData.get('Gender').toLowerCase(),
    //    Contact: formData.get('Contact'),
    //    City: formData.get('City').toLowerCase(),
    //    Religion: formData.get('Religion').toLowerCase(),
    //    BirthDate: formData.get('BirthDate'),
    //    ProofPayment: proofPaymentFileName,
    //    PaymentDate: formData.get('PaymentDate'),
    //    EntryDate: formData.get('EntryDate'),
    //    OutDate: outDateValue,
    //    Amount: amountValue,
    //    RoomId: formData.get('RoomId'),

    //}


    $.ajax({
        url: 'https://localhost:7095/api/Transaction/NewTransaction',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (res) {
            console.log(res);
            window.location.reload()
        },
        error: function (err) {
            console.log(err)
        }
    })
}


//admin validasi payment yang status masih on hold
function approvePayment(paymentId) {

    Swal.fire({
        title: 'Simpan Transaksi Pembayaran',
        text: "Anda yakin pembayaran sudah valid?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, simpan!',
        cancelButtonText: 'batal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7095/api/Payment/${paymentId}`,
                type: 'PATCH',
                contentType: 'application/json',
                success: function (res) {
                    console.log(res)
                    window.location.reload()
                }
            })

            Swal.fire(
                'Tersimpan!',
                'Transaksi pembayaran telah tersimpan.',
                'sukses'
            ).then(result => {
                if (result.isConfirmed) {
                    document.location.reload()
                }
            })
        }
    })


}

//admin reject payment status masih on hold
function rejectPayment(paymentId) {
    Swal.fire({
        title: 'Hapus Transaksi Pembayaran',
        text: "Transaksi pembayan akan terhapus!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, hapus!',
        cancelButtonText: 'batal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `https://localhost:7095/api/Payment/${paymentId}`,
                type: 'DELETE',
                contentType: 'application/json',
                success: function (res) {
                    console.log(res)

                }
            })

            Swal.fire(
                'Terhapus!',
                'Transaksi pembayaran telah terhapus.',
                'sukses'
            ).then(result => {
                if (result.isConfirmed) {
                    document.location.reload()
                }
            })
        }
    })


}




