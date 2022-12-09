$(document).ready(function () {
    $('#roomData').DataTable({
        ajax: {
            url: 'https://localhost:7095/api/DetailRoom',
            dataSrc: 'Data',
            //"pageLength": 5,
            //"headers": {
            //    //'Content-Type': 'application/x-www-form-urlencoded'
            //    'Authorization': "Bearer " + sessionStorage.getItem("token")
            //},
            "type": "GET",
        },
        //dataType: 'json',
        //success: function (result) {
        //    console.log(result);
        //    console.log(result.Data)
        //callback({ data: result.data })
        //        result.data.forEach((element, key) => {
        //            $.ajax({
        //                url: `https://localhost:7095/api/RoomPrice/${element.RoomPriceId}`,
        //                async: false,
        //                type: 'GET',
        //                dataType: 'json',
        //                success: function (res) {
        //                    result.data[key].Price = new String(res.Data.Price);
        //                    res.Data.Price;

        //                    //data[key].Price;
        //                    //console.log(data.Price);
        //                    console.log(result.data[key].Price);
        //                    console.log(res.Data.Price);
        //                }
        //            })
        //        }),
        //            result.data.forEach((element, key) => {

        //                $.ajax({
        //                    url: `https://localhost:7095/api/Payment/${element.PaymentId}`,
        //                    async: false,
        //                    type: 'GET',
        //                    dataType: 'Json',
        //                    success: function (res) {
        //                        //result.data[key].Id = new float(res.Data.Id);
        //                        console.log(res.Data.Id)
        //                    }

        //                })
        //            });
        //    }
        //})
        columns: [
            {
                data: 'Id', name: 'Id',
                "render": function (data, type, row, meta) {
                    return `<a data-toggle="modal" data-target="#modal-popout" onclick="detailKamar(${data})">Kamar ${data}</a>`;

                    console.log(data);
                }
            },
            {
                data: 'Floor', name: 'Floor'
            },
            {
                data: 'RoomPrice.Price', name: 'RoomPrice.Price',
            },
            {   
                data: 'Payment.Order.OutDate', name: 'Order.OutDate',
                "render": function (data, type, row, meta) {
                    if (data== null) {
                        return `<p>Kosong</p>`
                    } else {
                        return `<p>${data}</p>`
                    }

                }
            },
            {
                data: 'Status',
            },
            {
                data: 'Description',
            },
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return `  <td class="text-center">
                          <div class="btn-group">
                              <button type="button" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#modal-popout" title="Edit" onclick="editDetailKamar(${data})">
                                  <i class="fa fa-pencil"></i>
                              </button>
                              <button type="button" class="btn btn-sm btn-secondary" data-toggle="tooltip" title="Delete" onclick="deleteFacility('${data}')">
                                   <i class="fa fa-times"></i>
                             </button>
                          </div>
                     </td>`;
                }
            },
            //console.log(RoomPrice)
            //{
            //    data: 'PaymetId',
            //}
        ],
    });
});

$(document).ready(function () {
    let temp = "";
    $.ajax({
        url: `https://localhost:7095/api/DetailRoom`,
        type: "GET",
        async: false
    }).done((res) => {
        let allRooms = [];
        res.Data.forEach(room => {
            if (room.Status == false) {
                allRooms.push(room);
            }
        });
        allRooms.forEach(room => {
            $.ajax({
                url: `https://localhost:7095/api/RoomPicture`,
                type: "GET",
                async: false
            }).done((rest) => {
                let allRoomKosongWithPicture = [];
                for (let pict of rest.Data) {
                    if (pict.RoomId == room.Id) {
                        let objBaru = { ...room, ...pict }
                        allRoomKosongWithPicture.push(objBaru);
                        break;
                    }
                };
                //rest.data.forEach(pict => {
                //    //console.log(room)
                //    if (pict.RoomId == room.Id) {
                //        let objBaru = { ...room, ...pict }
                //        allRoomKosongWithPicture.push(objBaru);
                //    }
                //});
                $.each(allRoomKosongWithPicture, function(key, val) {
                    console.log(val)
                    temp += `<div class="col-md-6 col-xl-4 js-appear-enabled animated fadeIn" data-toggle="appear">
                    <div class="block block-rounded">
                    <div class="block-content p-0 overflow-hidden">
                        <a class="img-link" href="be_pages_real_estate_listing.html">
                            <img class="img-fluid rounded-top"  src="${val.Name}" alt="">
                        </a>
                        </a>
                    </div>
                    <div class="block-content border-bottom">
                        <h4 class="font-size-h5 mb-10">Informasi: ${val.Description}</h4>
                        <h5 class="font-size-h1 font-w300 mb-5"></h5>
                        <p class="text-muted">
                            <i class="fa fa-map-pin mr-5"></i> Fasilitas : ${val.Facility}
                        </p>
                        <p class="text-muted">
                            <i class="fa fa-dollar mr-5"></i> Harga : ${val.RoomPrice.Price}
                        </p>
                    </div>
                    <div class="block-content border-bottom">
                        <div class="row">
                            <div class="col-6">
                                <p>
                                    <i class="fa fa-fw fa-bed text-muted mr-5"></i> <strong>2</strong> Bedrooms
                                </p>
                            </div>
                            <div class="col-6">
                                <p>
                                    <i class="fa fa-fw fa-bath text-muted mr-5"></i> <strong>1</strong> Bathroom
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="block-content block-content-full">
                        <div class="row">
                            <div class="col-6">
                                <a class="btn btn-sm btn-hero btn-noborder btn-secondary btn-block" asp-controller="Landing" asp-action="RentDetail">
                                    Details
                                </a>
                            </div>
                            <div class="col-6">
                                <a class="btn btn-sm btn-hero btn-noborder btn-primary btn-block" href="be_pages_real_estate_listing_new.html">
                                    Edit
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;   
                }) 
                console.log(temp);
                document.getElementById("dataCard").innerHTML = temp;
            })
        })
    })
})

function detailKamar(id) {
    $.ajax({
        url: `https://localhost:7095/api/Room/${id}`,
        type: "GET"
    }).done((res) => {
        console.log(res);
        let temp = "";
        temp += ` <div class="js-wizard-simple block">
                                <ul class="nav nav-tabs nav-tabs-alt nav-fill" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#wizard-simple2-step1" data-toggle="tab">1. Facility</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#wizard-simple2-step3" data-toggle="tab" onclick="getImage(${id})">2. Picture</a>
                                    </li>
                                </ul>
                                <form action="be_forms_wizard.html" method="post">
                                    <div class="block-content block-content-full tab-content" style="min-height: 267px;">
                                        <div class="tab-pane active" id="wizard-simple2-step1" role="tabpanel">
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="wizard-simple2-firstname" name="wizard-simple2-firstname" value="Kamar ${id}" readonly>
                                                    <label for="wizard-simple2-firstname">ID</label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="wizard-simple2-lastname" name="wizard-simple2-lastname" value="${res.Data.Floor}" readonly>
                                                    <label for="wizard-simple2-lastname">FLOOR</label>
                                                </div>
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="email" id="wizard-simple2-email" name="wizard-simple2-email" value="${res.Data.Facility}" readonly>
                                                    <label for="wizard-simple2-email">Description</label>
                                                </div>
                                            </div>
                                          
                                        </div>
                                    <div class="block-content block-content-sm block-content-full bg-body-light">
                                        <div class="row">
                                            <div class="col-6 text-right">
                                                <button type="submit" class="btn btn-alt-primary d-none" data-wizard="finish">
                                                    <i class="fa fa-check mr-5"></i> Submit
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>`;
        $("#displayDetail").html(temp);
        console.log(id);
        //window.location.replace("../adam/DetailKamar")
    }).fail((err) => {
        console.log(err);
    });

}

$.ajax({
    url: `https://localhost:7095/api/DetailRoom`,
    type: "GET"
}).done((res) => {
    console.log(res.Data.length);
    console.log(res.Data);
    let yes = 0;
    let no = 0;
     $.each(res.Data, function (key, val) {
         if (val.Status == false) {
             yes++;
         } else {
             no++;
         }
     });
    console.log(yes);
    console.log(no);
    let avail = "";
    avail += `       <div class="font-size-h2 font-w700 mb-0 text-info">${yes}</div>
                        <div class="font-size-sm font-w600 text-uppercase text-muted">AVAILABLE</div>
                       `;


    let notavail = "";
    notavail += `<div class="font-size-h2 font-w700 mb-0 text-danger">${no}</div>
                        <div class="font-size-sm font-w600 text-uppercase text-muted">NOT AVAILABLE</div>
                       `;
    //console.log(res.data.length);
    //var jumlah = res.data.length;
    let count = res.Data.length;
    //$.each(res.Data, function (key, val) {
    //    count++
    //});
    //console.log(count);
    let temp = "";
    temp += `       <div class="font-size-h2 font-w700 mb-0 text-info">${count}</div>
                        <div class="font-size-sm font-w600 text-uppercase text-muted">TOTAL ROOM</div>

                       `;
    $("#countData").html(temp);
    $("#Ada").html(avail);
    $("#Gakada").html(notavail);
    //console.log($("#countData").html(temp));
}).fail((err) => {
    console.log(err);
});


function getImage(id) {
    $.ajax({
                url: `https://localhost:7095/api/RoomPicture`,
                //dataSrc: 'data',
                //"headers": {
                //    //'Content-Type': 'application/x-www-form-urlencoded'
                //    'Authorization': "Bearer " + sessionStorage.getItem("token")
                //},
                type: "GET",
    }).done((res) => {
        console.log(res);
        let temp = "";
        let name = "";
        $.each(res.Data, function (key, val) {
            if (val.RoomId == id) {
                name = val.Name
            }
        })
        console.log(name)
        temp += ` <div class="js-wizard-simple block">
                                <ul class="nav nav-tabs nav-tabs-alt nav-fill" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link" href="#wizard-simple2-step1" data-toggle="tab" onclick="detailKamar(${id})" >1. Facility</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#wizard-simple2-step3" data-toggle="tab">2. Picture</a>
                                    </li>
                                </ul>

                             <div class="col-md-4 animated fadeIn">
                                                        <div class="options-container">
                                <img class="img-fluid options-item" src="${name}" alt="">

                                                            <img class="img-fluid options-item" alt="">
                                                            <div class="options-overlay bg-black-op-75">
                                                                <div class="options-overlay-content">
                                                                    <h3 class="h4 text-white mb-5">Image</h3>
                                                                    <h4 class="h6 text-white-op mb-15">More Details</h4>
                                                                    <a class="btn btn-sm btn-rounded btn-alt-primary min-width-75" href="javascript:void(0)">
                                                                        <i class="fa fa-pencil"></i> Edit
                                                                    </a>
                                                                    <a class="btn btn-sm btn-rounded btn-alt-danger min-width-75" href="javascript:void(0)">
                                                                        <i class="fa fa-times"></i> Delete
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                            </div>`;
        $("#displayDetail").html(temp);
        //console.log(res.Data.Id);
        //window.location.replace("../adam/DetailKamar")
    }).fail((err) => {
        console.log(err);
    });

}

function editDetailKamar(id) {
    $.ajax({
        url: `https://localhost:7095/api/DetailRoom/${id}`,
        type: "GET"
    }).done((res) => {
        console.log(res);
        let temp = "";
        temp += ` <div class="js-wizard-simple block">
                                <ul class="nav nav-tabs nav-tabs-alt nav-fill" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#wizard-simple2-step1" data-toggle="tab">1. Facility</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="#wizard-simple2-step3" data-toggle="tab">3. Picture</a>
                                    </li>
                                </ul>

                                <form action="be_forms_wizard.html" method="post">
                                    <div class="block-content block-content-full tab-content" style="min-height: 267px;">
                                        <div class="tab-pane active" id="wizard-simple2-step1" role="tabpanel">
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailId" name="wizard-simple2-firstname" value="Kamar ${res.Data.Id}" readonly>
                                                    <label for="wizard-simple2-firstname">Nama Kamar</label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailFloor" name="wizard-simple2-lastname" value="${res.Data.Floor}">
                                                    <label for="wizard-simple2-lastname">Floor</label>
                                                </div>  <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailPrice" name="wizard-simple2-lastname" value="${res.Data.Price}">
                                                    <label for="wizard-simple2-lastname">Room Price</label>
                                                </div>
                                            </div>  
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailFacility" name="wizard-simple2-lastname" value="${res.Data.Facility}">
                                                    <label for="wizard-simple2-lastname">Facility</label>
                                                </div>  </div>  <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailPaymentId" name="wizard-simple2-lastname" value="${res.Data.PaymentId}">
                                                    <label for="wizard-simple2-lastname">Payment ID</label>
                                                </div>
                                            <div class="form-group">
                                                <div class="form-material floating open">
                                                    <input class="form-control" type="text" id="detailDescription" name="wizard-simple2-email" value="${res.Data.Description}">
                                                    <label for="wizard-simple2-email">Description</label>
                                                </div>
                                    <div class="block-content block-content-sm block-content-full bg-body-light">
                                        <div class="row">
                                            <div class="col-6 text-right">
                                                <button type="button" class="btn btn-alt-secondary" data-wizard="next" onclick="saveEditKamar('${res.Data.Id}')">
                                                    Submit <i class="fa fa-angle-right ml-5"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>`;
        $("#displayDetail").html(temp);
        console.log(res.Data.Id);
        //window.location.replace("../adam/DetailKamar")
    }).fail((err) => {
        console.log(err);
    });

}

function saveEditKamar(id) {
    var Id = id;
    var Floor = document.getElementById("detailFloor").value;
    var RoomPriceId = document.getElementById("detailPrice").value;
    var Facility = document.getElementById("detailFacility").value;
    var PaymentId = document.getElementById("detailPaymentId").value;
    var Description = document.getElementById("detailDescription").value;
    //var Status = document.getElementById("detailStatus").value;

    var result = { Id, Floor, RoomPriceId, PaymentId, Description, Facility };
    console.log(result);
    $.ajax({
        url: `https://localhost:7095/api/Room/${id}`,
        type: "PUT",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(result),
        success: function () {
            Swal.fire(
                'Good job!',
                'You clicked the button!',
                'success'
            );
            setTimeout(function () {
                location.reload();
            }, 5000);
        }, error: function () {

        }
    });
}

function deleteFacility(id) {
    $.ajax({
        url: `https://localhost:7095/api/Room/${id}`,
        type: "DELETE",
        success: function (data) {
            Swal.fire(
                'Good job!',
                'You clicked the button!',
                'success'
            );
            setTimeout(function () {
                location.reload();
            }, 5000);
        }
    });
}

function addRoom() {
    $.ajax({
        url: `https://localhost:7095/api/Room`,
        type: "GET"
    }).done((res) => {
        console.log(res.Data);
        let temp = "";
        temp += `<div class="js-wizard-simple block">
                                <ul class="nav nav-tabs nav-tabs-alt nav-fill" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#wizard-simple2-step1" data-toggle="tab">1. Facility</a>
                                    </li>
                                </ul>

                                <form action="be_forms_wizard.html" method="post">
                                    <div class="block-content block-content-full tab-content" style="min-height: 267px;">
                                        <div class="tab-pane active" id="wizard-simple2-step1" role="tabpanel">
                                            <input type="hidden" class="form-control" id="hideId" readonly placeholder="" value="0">
                                            <div class="tab-pane" id="wizard-simple2-step2" role="tabpanel">
                                                <div class="form-group">
                                                    <div class="form-material floating">
                                                        <div class="form-group">
                                                            <div class="form-material floating open">
                                                                <input class="form-control" type="text" id="val-floor" name="wizard-simple2-firstname" value="" >
                                                                <label for="wizard-simple2-firstname">FLOOR</label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="form-material floating open">
                                                             <select class="form-control" id="selectedRoom" name="selectedRoom" style="width: 100%;" data-placeholder="Choose one.."></select>
                                                                <label for="wizard-simple2-email" for="selectedRoom">ROOM TYPE</label>
                                                            </div>
                                                        </div><div class="form-group">
                                                            <div class="form-material floating open">
                                                                <input class="form-control" type="email" id="val-facility" name="wizard-simple2-email" value="" >
                                                                <label for="wizard-simple2-email">Facility</label>
                                                            </div>
                                                        </div><div class="form-group">
                                                            <div class="form-material floating open">
                                                                <input class="form-control" type="email" id="val-description"  value="" >
                                                                <label for="wizard-simple2-email">Description</label>
                                                            </div>
                                                        </div> 
                                                        </div><div class="form-group">
                                                            <div class="form-material floating open">
                                                                 <label for="formFile" class="form-label">Upload Room Picture</label>
                                              <input class="form-control" type="file" id="uploadImageRoom">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block-content block-content-sm block-content-full bg-body-light">
                                                <div class="row">
                                                    <div class="col-6 text-right">
                                                        <button type="submit" class="btn btn-alt-primary d-none" data-wizard="finish">
                                                            <i class="fa fa-check mr-5"></i> Submit
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                </form>
                            </div>
            `;
        $("#roomCreate").html(temp);
        console.log(res);
    }).fail((err) => {
        console.log(err);
    })

    $.ajax({
        url: `https://localhost:7095/api/RoomPrice`,
        type: 'GET',
        success: function (res) {
            console.log(res.Data)
            let temp = `<option value="" disabled selected>pilih kamar</option>`
            res.Data.forEach(x => {
                temp += `<option value="${x.Id}">Tipe ${x.RoomType}</option>`
                console.log(res.Data)
            })
            document.getElementById('selectedRoom').innerHTML = temp
        }
    })
}


function addImage() {
    $.ajax({
        url: `https://localhost:7095/api/Room`,
        type: "GET"
    }).done((res) => {
        console.log(res.Data);
        let temp = "";
        temp += `
                          <div class="js-wizard-simple block">
                                <ul class="nav nav-tabs nav-tabs-alt nav-fill" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link" href="#wizard-simple2-step1" data-toggle="tab" onclick="addRoom()">1. Facility</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link active" href="#wizard-simple2-step3" data-toggle="tab">2. Picture</a>
                                    </li>
                                </ul>

                                <form action="be_forms_wizard.html" method="post">
                                    <div class="block-content block-content-full tab-content" style="min-height: 267px;">
                                        <div class="tab-pane active" id="wizard-simple2-step1" role="tabpanel">
                                          <input type="hidden" class="form-control" id="hideId" readonly placeholder="" value="0">
                                            <div class="tab-pane" id="wizard-simple2-step2" role="tabpanel">
                                            <div class="mb-3">
                                              <label for="formFile" class="form-label">Upload Room Picture</label>
                                              <input class="form-control" type="file" id="uploadImageRoom">
                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="block-content block-content-sm block-content-full bg-body-light">
                                                <div class="row">
                                                    <div class="col-6 text-right">
                                                        <button type="submit" class="btn btn-alt-primary d-none" data-wizard="finish">
                                                            <i class="fa fa-check mr-5"></i> Submit
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                </form>
                            </div>


            `;
        $("#roomCreate").html(temp);
        console.log(res);
    }).fail((err) => {
        console.log(err);
    })
}


function saveRoom() {
    const data = new FormData();
    data.append("Id", $('#hideId').val());
    data.append("Floor", $('#val-floor').val());
    data.append("RoomPriceId", $('#selectedRoom').val());
    data.append("Status", false);
    data.append("PaymentId", 0);
    data.append("Facility", $('#val-facility').val());
    data.append("Description", $('#val-description').val());
    data.append("Description", $('#val-description').val());
    const file = $("#uploadImageRoom")[0].files[0];
    data.append("Image", file, file.name);
    console.log(data);

    //var Name = document.getElementById("facilityName").value;
    //var Id = document.getElementById("hideId").value;
    //var Floor = document.getElementById("val-floor").value;
    //var RoomPriceId = document.getElementById('selectedRoom').value;
    //var Status = false;
    //var PaymentId = null;
    //var Facility = document.getElementById("val-facility").value;
    //var Description = document.getElementById("val-description").value;

    //var result = { Id, Floor, RoomPriceId, Status, PaymentId, Facility, Description };
    //console.log(result)
    $.ajax({
        url: `https://localhost:7095/api/DetailRoom`,
        enctype: 'multipart/form-data',
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        success: function () {
            Swal.fire(
                'Good job!',
                'You clicked the button!',
                'success'
            );
            setTimeout(function () {
                location.reload();
            }, 5000);
        },
        error: function () {

        }
    })
}
//$(document).ready(function(){
//    $("button").click(function () {
//        alert($("li").length);
//    });
//});
$.ajax({
    url: `https://localhost:7095/api/Payment/`,
    type: "GET"
}).done((rest) => {
    let InCome = [];
    let HoldCome = [];
    let TotalIncome = 0;
    let TotalHoldCome = 0;
    console.log(rest);
    
    $.each(rest.Data, function (key, val) {
        if (val.Status == true) {
            InCome.push(val.Amount);
        } else {
            HoldCome.push(val.Amount);
        }
    });

    for (x in InCome) {
        TotalIncome += InCome[x];
    }
    for (x in HoldCome) {
        TotalHoldCome += HoldCome[x];
    }
    console.log(TotalHoldCome);
    console.log(TotalIncome);


    var options = {
        series: [{
            name: 'Hold',
            data: [TotalHoldCome]
        }, {
            name: 'Income',
            data: [TotalIncome]
        }],
        chart: {
            type: 'bar',
            height: 350
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
            },
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: ['Data'],
        },
        yaxis: {
            title: {
                text: 'Rp'
            }
        },
        fill: {
            opacity: 1
        },
        tooltip: {
            y: {
                formatter: function (val) {
                    return "Rp " + val + " Rupiah"
                }
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#stats"), options);
    chart.render();


}).fail((err) => {
    console.log(err);
})

$.ajax({
    url: `https://localhost:7095/api/Occupant/`,
    type: "GET"
}).done((rest) => {
    let Gender = [];
    $.each(rest.Data, function (key, val) {
        Gender.push(val.Gender);
    });
    console.log(Gender);
    const counts = {};
    const sampleArray = Gender;
    sampleArray.forEach(function (x) { counts[x] = (counts[x] || 0) + 1; });
    console.log(counts);
    let Cseries = [];
    let labels = [];
    for (const key in counts) {
        labels.push(key);
        Cseries.push(counts[key]);
    }
    console.log(labels);
    console.log(Cseries);

    var options = {
        series: Cseries,
        chart: {
            width: 380,
            type: 'pie',
        },
        labels: labels,
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }]
    };

    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();

    //console.log(rest);
    //console.log(divisionId);
}).fail((err) => {
    console.log(err);
})