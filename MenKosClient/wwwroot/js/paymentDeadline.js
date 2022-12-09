$(document).ready(function () {
    $('#deadlineTable').DataTable({
        ajax: {
            url: 'https://localhost:7095/api/transaction/paymentdeadline',
            dataSrc: 'Data'
        },
        columns: [
            {
                data: "RoomId",
                render: (data, type, row, meta) => {
                    return `Kamar no ${data}`;
                }
            },
            {
                data: 'Payment.Order.Occupant.Name',
                render: (data) => {
                    return data;
                }
            },

            {
                data: "Payment.Order.Occupant.Contact",
                render: (data) => {
                    return data;
                }
            },
            {
                data: "Payment.Order.OutDate",
                render: (data) => {
                    return new Date(data).toLocaleDateString('id-ID', {
                        weekday: 'long',
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                    });
                }
            },
          

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





