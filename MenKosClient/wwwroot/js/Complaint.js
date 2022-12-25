//let token = window.sessionStorage.getItem("token")

//parsedToken = parseJwt(token)

//console.log(parsedToken)

//document.getElementById('daftarTagihanOccupant').href = `/user/PaymentDeadline/${parsedToken.OccupantId}`


$(document).ready(function () {
    $('#ListKeluhan').DataTable({

        ajax: {
            url: 'https://localhost:7095/api/Complaint',
            dataSrc: 'Data',
        },
        columns: [
            {
              
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1
                }

            },
            { data: 'Problem' },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    if (data.Status == false) {
                        return `<span class="badge badge-warning">Menunggu</span>`
                    } else {
                        return `<span class="badge badge-success">Selesai</span>`
                    }
                   
                }
            },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    if (data.Reply == null) {
                        return ` <button type="button" class="btn btn-sm btn-secondary js-tooltip-enabled" data-toggle="tooltip" title="" data-original-title="Edit">
                                     <i class="si si-clock"></i>
                                 </button>`
                    } else {
                        return ` <button type="button" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#jawaban" onclick="SetIdAnswer('${data.Id}')">
                                    <i class="si si-envelope-letter"></i>
                                 </button>`
                    }

                }
            }
        ],

        dom: "bfrtip",
        buttons: [
            "colvis"
        ]


});

});

$(document).ready(function () {
    $('#ListKeluhanAdmin').DataTable({

        ajax: {
            url: 'https://localhost:7095/api/Complaint',
            dataSrc: 'Data',
        },
        columns: [
            {

                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1
                }
            },
            { data: 'Problem' },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    if (data.Status == false) {
                        return `<span class="badge badge-warning">Menunggu</span>`
                    } else {
                        return `<span class="badge badge-success">Selesai</span>`
                    }

                }
            },
            {
                data: 'RoomId'
                
            },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    if (data.Reply == null) {
                          return ` <button type="button" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#jawabKeluhan">
                                     <i class="fa fa-pencil-square-o"></i>
                                 </button>
                                        <div class="modal fade" id="jawabKeluhan" tabindex="-1" role="dialog" aria-labelledby="modal-popout" style="display: none;" aria-hidden="true">
                                            <div class="modal-dialog modal-dialog-popout" role="document">
                                                <div class="modal-content">
                                                    <div class="block block-themed block-transparent mb-0">
                                                        <div class="block-content">
                                                            <div class="form-material">
                                                                <textarea class="form-control" id="menjawabKeluhan" name="material-textarea-large" rows="8" placeholder="Please add a comment"></textarea>
                                                                <label for="material-textarea-large">Jawab Keluhan</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-alt-secondary" data-dismiss="modal">Close</button>
                                                        <button type="button" class="btn btn-alt-success" data-dismiss="modal" id="btnJawabanDariAdmin" onclick="InputJawabanId('${data.Id}','${data.Problem}','${data.RoomId}')">
                                                            <i class="fa fa-check"></i> Kirim Jawaban
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>`
                    } else {
                        return ` <button type="button" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#terjawab" onclick="SetIdAnswer2('${data.Id}')">
                                    <i class="si si-envelope-letter"></i>
                                 </button>`
                    }

                }
            }
        ],

        dom: "bfrtip",
        buttons: [
            "colvis"
        ]


    });

});

function SetIdAnswer(Id) {
    $.ajax({
        url: `https://localhost:7095/api/Complaint/${Id}`,
        type: "GET"
    }).done((res) => {
        console.log(res.Data.Reply);
       // console.log(res.Data.Reply);
        let Jawaban = "";
        Jawaban += `
        ${res.Data.Reply}
        `

        $("#jawabanKeluhan").html(Jawaban);
    });
}

function SetIdAnswer2(Id) {
    $.ajax({
        url: `https://localhost:7095/api/Complaint/${Id}`,
        type: "GET"
    }).done((res) => {

        // console.log(res.Data.Reply);
        let Jawaban = "";
        Jawaban += `
        ${res.Data.Reply}
        `

        $("#jawabanKeluhanAdmin").html(Jawaban);
    });
}
//('${data.Id}','${data.Problem}','${data.CreatedAt}','${data.RoomId}'

function InputJawabanId(Id, Masalah, Kamar) {
    var Id = parseInt(Id);
    var Problem = Masalah;
    var CreatedAt = "2022-12-09T01:27:35.944";
    var RoomId = 4;
    var Reply = $('#menjawabKeluhan').val();
    var Status = true;
    var data = { Id, CreatedAt, Problem, Reply, Status, RoomId };
    console.log(data)
    $.ajax({
        url: `https://localhost:7095/api/Complaint/${RoomId}`,
        type: "PUT",
        contentType: "application/json",
        dataTpe: "json",
        data: JSON.stringify(data),
        //cache: false,
        success: function () {
            alert("sukses");
            window.location.reload();
        }, error: function (jqXHR, status) {
            console.log(jqXHR.responseJSON)
            console.log(jqXHR)
            console.log(status)
        }
    })
}



$(document).ready(function () {
    $('#btnBuatKeluhan').on('click', function () {
        //var date = new Date();
        var CreatedAt = "2022-12-02T01:31:45.791";
        var Problem = $('#buatKeluhan').val();
        var Reply = null;
        var Status = false;
        var RoomId = 4;
        var data = { CreatedAt, Problem, Reply, Status, RoomId };
        $.ajax({
            url: "https://localhost:7095/api/Complaint/",
            type: "POST",
            contentType: "application/json",
            dataTpe: "json",
            data: JSON.stringify(data),
            //cache: false,
            success: function () {
                alert("Sukses");
                window.location.reload();
            }, error: function () {
            }
        })

    });
});



//$.ajax({
//    url: `https://localhost:7095/api/Occupant/`,
//    type: "GET"
//}).done((rest) => {
//    let Gender = [];
//    $.each(rest.data, function (key, val) {
//        Gender.push(val.Gender);
//    });
//    console.log(Gender);
//    const counts = {};
//    const sampleArray = Gender;
//    sampleArray.forEach(function (x) { counts[x] = (counts[x] || 0) + 1; });
//    console.log(counts);
//    let Cseries = [];
//    let labels = [];
//    for (const key in counts) {
//        labels.push(key);
//        Cseries.push(counts[key]);
//    }
//    console.log(labels);
//    console.log(Cseries);

//    var options = {
//        series: Cseries,
//        chart: {
//            width: 380,
//            type: 'pie',
//        },
//        labels: labels,
//        responsive: [{
//            breakpoint: 480,
//            options: {
//                chart: {
//                    width: 200
//                },
//                legend: {
//                    position: 'bottom'
//                }
//            }
//        }]
//    };

//    var chart = new ApexCharts(document.querySelector("#chart"), options);
//    chart.render();

//    //console.log(rest);
//    //console.log(divisionId);
//}).fail((err) => {
//    console.log(err);
//})

//$.ajax({
//    url: `https://localhost:7095/api/Payment/`,
//    type: "GET"
//}).done((rest) => {
//    let InCome = [];
//    let HoldCome = [];
//    let TotalIncome = 0;
//    let TotalHoldCome = 0;
//    $.each(rest.data, function (key, val) {
//        if (val.Status == true) {
//            InCome.push(val.Amount);
//        } else {
//            HoldCome.push(val.Amount);
//        }
//    });
    
//    for(x in InCome)
//    {
//        TotalIncome += InCome[x];
//    }
//    for (x in HoldCome) {
//        TotalHoldCome += HoldCome[x];
//    }


//    var options = {
//        series: [{
//            name: 'Hold',
//            data: [TotalHoldCome]
//        }, {
//            name: 'Income',
//            data: [TotalIncome]
//        }],
//        chart: {
//            type: 'bar',
//            height: 350
//        },
//        plotOptions: {
//            bar: {
//                horizontal: false,
//                columnWidth: '55%',
//                endingShape: 'rounded'
//            },
//        },
//        dataLabels: {
//            enabled: false
//        },
//        stroke: {
//            show: true,
//            width: 2,
//            colors: ['transparent']
//        },
//        xaxis: {
//            categories: ['Dec'],
//        },
//        yaxis: {
//            title: {
//                text: 'Rp'
//            }
//        },
//        fill: {
//            opacity: 1
//        },
//        tooltip: {
//            y: {
//                formatter: function (val) {
//                    return "Rp " + val + " Rupiah"
//                }
//            }
//        }
//    };

//    var chart = new ApexCharts(document.querySelector("#stats"), options);
//    chart.render();
    
    
//}).fail((err) => {
//    console.log(err);
//})

function Gambar() {
    $.ajax({
        url: `https://localhost:7095/api/Posts`,
        type: "GET"
    }).done((rest) => {
        let pict = "";
        
        $.each(rest.data, function (key, val) {
            if (val.Id == 16) {
                pict += `<img src="${val.ImagePath}" class="d-block w-100" height="300" alt="...">`
                console.log(val.ImagePath);
                
            }
        });
        $(".picture").html(pict);
        console.log(pict);
    }).fail((err) => {
        console.log(err)
    })

}

