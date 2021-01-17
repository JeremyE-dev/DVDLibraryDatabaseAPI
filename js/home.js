$(document).ready(function() {
// make ajax call to dvdv display table here
  getDVDInfo();
$('#add-form').hide();
$('#edit-form').hide();
$('#display-dvd-details-form').hide();

$('#nav-create-dvd-button').on('click', function() {
  showCreateDVDWindow();
});

$('#display-details-back-button').on('click', function() {
  //clearDVDDisplay();
    $('#DVDdisplaytableDiv').show();
    $('#navbarDiv').show();
    $('#add-form').hide();
    $('#edit-form').hide();
    $('#display-dvd-details-form').hide();
});

//Added validation to add button
$('#add-button').on('click', function() {
  var haveValidationErrors = checkAndDisplayValidationErrors_Create(($('#add-form').find('input')));

  if(haveValidationErrors) {
      //alert('there was an error');
    return false;
  }

  postDVDInfo();
  clearDVDDisplay();
  $('#navbarDiv').show();
  $('#DVDdisplaytableDiv').show();
  $('#add-form').hide();

}); //closes jQuery code

$('#cancel-add-button').on('click', function() {
  //alert('test');
  $('#DVDdisplaytableDiv').show();
  $('#add-form').hide();
  $('#navbarDiv').show();
}); //closes jQuery code

$('#search-button').on('click', function() {

//alert($('#search-term').find().val());
//var haveValidationErrors = checkAndDisplayValidationErrors_Search(($('#search-term').find('input')));

  //if(haveValidationErrors) {
      //alert('there was an error');
  //  return false;
  //}




  var searchDropdownValue = $('#search-category-dropdown').val();
  if(searchDropdownValue == "Title") {
    getDVDByTitle($('#search-term').val());
    $('#DVDdisplaytableDiv').show();
    $('#add-form').hide();
    $('#navbarDiv').show();
  }
  else if(searchDropdownValue == "Release Date") {
    getDVDByYear($('#search-term').val());
    $('#DVDdisplaytableDiv').show();
    $('#add-form').hide();
    $('#navbarDiv').show();

  }
  else if(searchDropdownValue == "Director") {
    getDVDByDirector($('#search-term').val());
    $('#DVDdisplaytableDiv').show();
    $('#add-form').hide();
    $('#navbarDiv').show();

  }
  else if(searchDropdownValue == "Rating"){
    getDVDByRating($('#search-term').val());
    $('#DVDdisplaytableDiv').show();
    $('#add-form').hide();
    $('#navbarDiv').show();
  }

});


}) //closes .ready


function showDvdDetails(dvdID) {
  $.ajax({
    type: 'GET',
    url: 'http://localhost:52639/dvds/get/' + dvdID,
    success: function(data, status) {
      //how do I add title to be the top of the page
      //$('#display-dvd-title').text(data.title);
      var title = data.Title;
      $('#display-dvd-title').text(title);
      $('#display-release-year-rating').val(data.releaseYear);
      $('#display-director-details').val(data.director);
      $('#display-rating-details').val(data.rating);
      $('#display-notes-details').val(data.Notes);
        //did not get the id,it is not used in form
        $('#DVDdisplaytableDiv').hide();
        $('#navbarDiv').hide();
        $('#edit-form').hide();
        $('#display-dvd-details-form').show();
        $('h2').text(title);

      },
      error: function() {
        getDVDInfo();
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.'));
      }
  });


}
function getDVDInfo() {
clearDVDDisplay();

  var DVDdisplaytable = $('#DVDdisplaycontentRows');
  $.ajax ({
    type: 'GET',
    url: 'http://localhost:52639/dvds/all',
    success: function(data, status) {
      $.each(data, function(index, dvd){
        var title = dvd.Title;
        var releaseYear = dvd.releaseYear;
        var director = dvd.director;
        var rating = dvd.rating;
        var notes = dvd.Notes;
        var dvdID = dvd.DvdId;

        var row = '<tr>';
            //row += '<td>' + title + '</td>';
            row += '<td><a onclick="showDvdDetails('+ dvdID +')"><u>'+title+'</u></a></td>';

            row += '<td>' + releaseYear + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            //row += '<td><a onclick = "getDVDToEdit(' + dvdID + ')">Edit + "|"</a></td>'; // "|" '<td><a onclick = "deleteDVD(' + dvdID + ')">Delete</a></td>';
            //row += '<td><a onclick = "confirmDelete(' + dvdID + ')">Delete</a></td>';
            row += '<td><a onclick="getDVDToEdit('+ dvdID +')"> <u> Edit</u> </a>'+ " | " + '<a onclick="deleteDVD('+ dvdID +')"> <u> Delete</u></a></td>';

            DVDdisplaytable.append(row);
        }); // end success:function
      },
      error: function() {
        //alert('failure');
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
      }

  }); //end ajax call
}

function postDVDInfo() {
  $.ajax({
    type: 'POST',
    url: 'http://localhost:52639/dvds/add',
    data: JSON.stringify({
      Title: $('#add-title').val(),
      releaseYear: $('#add-release-year').val(),
      director: $('#add-director').val(),
      rating: $('#add-rating').val(),
      Notes: $('#add-notes').val()
    }),
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    'dataType' : 'json',
    success: function() {
    $('#errorMessages').empty();//alert('SUCCESS');
    getDVDInfo();
    //$('#DVDdisplaytableDiv').show();
    //$('#add-form').hide();

    },
    error: function() {
      getDVDInfo();
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
    }
  });

}

function showCreateDVDWindow(){
  $('#add-form').show();
  $('#DVDdisplaytableDiv').hide();
  $('#navbarDiv').hide();
  $('#edit-form').hide();

}

function getDVDToEdit(dvdID) { //connected to 'Edit' on table
//add check for validation error code here
  $.ajax({
    type: 'GET',
    url: 'http://localhost:52639/dvds/get/' + dvdID,
    success: function(data, status) {
      var headerTitle = data.Title;
      $('#edit-title').val(data.Title);
      $('#edit-release-year').val(data.releaseYear);
      $('#edit-director').val(data.director);
      $('#edit-rating').val(data.rating);
      $('#edit-notes').val(data.Notes);
      $('h2').text('Edit Dvd: ' +  headerTitle);
        //did not get the id,it is not used in form
        $('#DVDdisplaytableDiv').hide();
        $('#navbarDiv').hide();
        $('#edit-form').show();

      },
      error: function() {
        getDVDInfo();
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.'));
      }
  });

$('#save-changes-button').on('click', function() {
  var haveValidationErrors = checkAndDisplayValidationErrors_Edit(($('#edit-form').find('input')));

  if(haveValidationErrors) {
      //alert('there was an error');
    return false;
  }
 //alert('save changes pressed');
  //putEditedDVDInfo(dvdID);
  $.ajax({
    type: 'PUT',
    url: 'http://localhost:52639/dvds/update/' + dvdID,
    data: JSON.stringify({
      DvdId: dvdID,
      Title: $('#edit-title').val(),
      releaseYear: $('#edit-release-year').val(),
      director: $('#edit-director').val(),
      rating:$('#edit-rating').val(),
      Notes:$('#edit-notes').val()
    }),
    headers: {
      'Accept' : 'application/json',
      'Content-Type' : 'application/json'
    },
    'dataType': 'json',
    success: function() {
      clearDVDDisplay();
      $('#errorMessages').empty();
      getDVDInfo();
      $('#DVDdisplaytableDiv').show();
      $('#navbarDiv').show();
      $('#edit-form').hide();

    //alert('PUT success');
    },
    error: function() {
      getDVDInfo();
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
    }

  });
});
}

function deleteDVD(dvdId) {

  var confirm = window.confirm("Are you sure you want to delete this Dvd from your collection?");
  if(confirm == true) {
    $.ajax({
      type: 'DELETE',
      url:'http://localhost:52639/dvds/delete/' + dvdId,
      success: function(status) {
        //alert('DVD deleted');
        getDVDInfo();

      }
    });

  } else {
        getDVDInfo();
  }

}

function clearDVDDisplay() {
  $("#DVDdisplaycontentRows").empty();
}

function confirmDelete(dvdId) {
  var txt;
  var confirm = confirm("Are you sure you want to delete this Dvd from your collection?");
  if(confirm == true) {
    deleteDVD(dvdId);
  } else {
    getDVDInfo();
  }
}
// if dropdowm = title call getDVD by Title
// search term will be passed in as param
//title param will be the search Term
function getDVDByTitle(title) {
  clearDVDDisplay();

    var DVDdisplaytable = $('#DVDdisplaycontentRows');
    alert('mades it inside method - title is' + title);
    $.ajax ({
      type: 'GET',
      url: 'http://localhost:52639/dvds/title',
      //old url: 'http://localhost:52639/dvds/title/' + title,
      success: function(data, status) {
        $.each(data, function(index, dvd){
          var title = dvd.Title;
          var releaseYear = dvd.releaseYear;
          var director = dvd.director;
          var rating = dvd.rating;
          var notes = dvd.Notes;
          var dvdID = dvd.dvdId;

          var row = '<tr>';
              row += '<td>' + title + '</td>';
              row += '<td>' + releaseYear + '</td>';
              row += '<td>' + director + '</td>';
              row += '<td>' + rating + '</td>';
              //row += '<td><a onclick = "getDVDToEdit(' + dvdID + ')">Edit + "|"</a></td>'; // "|" '<td><a onclick = "deleteDVD(' + dvdID + ')">Delete</a></td>';
              //row += '<td><a onclick = "confirmDelete(' + dvdID + ')">Delete</a></td>';
              row += '<td><a onclick="getDVDToEdit('+ dvdID +')"> Edit</a>'+ " | " + '<a onclick="deleteDVD('+ dvdID +')"> Delete</a></td>';

              DVDdisplaytable.append(row);
          }); // end success:function
        },
        error: function() {
          //alert('failure');
        $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service. Please try again later.'));
        }

    }); //end ajax call

}


function getDVDByYear(year) {

  clearDVDDisplay();
  var DVDdisplaytable = $('#DVDdisplaycontentRows');
  alert('mades it inside method - year is: ' + year);
  $.ajax ({
    type: 'GET',
    url: 'http://localhost:52639/dvds/year/' + year,
    success: function(data, status) {
      $.each(data, function(index, dvd){
        var title = dvd.Title;
        var releaseYear = dvd.releaseYear;
        var director = dvd.director;
        var rating = dvd.rating;
        var notes = dvd.Notes;
        var dvdID = dvd.dvdId;

        var row = '<tr>';
            row += '<td>' + title + '</td>';
            row += '<td>' + releaseYear + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            //row += '<td><a onclick = "getDVDToEdit(' + dvdID + ')">Edit + "|"</a></td>'; // "|" '<td><a onclick = "deleteDVD(' + dvdID + ')">Delete</a></td>';
            //row += '<td><a onclick = "confirmDelete(' + dvdID + ')">Delete</a></td>';
            row += '<td><a onclick="getDVDToEdit('+ dvdID +')"> Edit</a>'+ " | " + '<a onclick="deleteDVD('+ dvdID +')"> Delete</a></td>';

            DVDdisplaytable.append(row);
        }); // end success:function
      },
      error: function() {
        //alert('failure');
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
      }

  }); //end ajax call
}


function getDVDByDirector(director) {
  clearDVDDisplay();
  var DVDdisplaytable = $('#DVDdisplaycontentRows');
  alert('mades it inside method - director is: ' + director);
  $.ajax ({
    type: 'GET',
    url: 'http://localhost:52639/dvds/get/director/' + director,
    //OLD URL url: 'http://localhost:52639/dvds/director/' + director,
    success: function(data, status) {
      $.each(data, function(index, dvd){
        var title = dvd.Title;
        var releaseYear = dvd.releaseYear;
        var director = dvd.director;
        var rating = dvd.rating;
        var notes = dvd.Notes;
        var dvdID = dvd.dvdId;

        var row = '<tr>';
            row += '<td>' + title + '</td>';
            row += '<td>' + releaseYear + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            //row += '<td><a onclick = "getDVDToEdit(' + dvdID + ')">Edit + "|"</a></td>'; // "|" '<td><a onclick = "deleteDVD(' + dvdID + ')">Delete</a></td>';
            //row += '<td><a onclick = "confirmDelete(' + dvdID + ')">Delete</a></td>';
            row += '<td><a onclick="getDVDToEdit('+ dvdID +')"> Edit</a>'+ " | " + '<a onclick="deleteDVD('+ dvdID +')"> Delete</a></td>';

            DVDdisplaytable.append(row);
        }); // end success:function
      },
      error: function() {
        //alert('failure');
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
      }

  }); //end ajax call
}

function getDVDByRating(rating) {
  clearDVDDisplay();
  var DVDdisplaytable = $('#DVDdisplaycontentRows');
  alert('mades it inside method - director is: ' + rating);
  $.ajax ({
    type: 'GET',
    url: 'http://localhost:52639/dvds/get/rating/' + rating,
    success: function(data, status) {
      $.each(data, function(index, dvd){
        var title = dvd.Title;
        var releaseYear = dvd.releaseYear;
        var director = dvd.director;
        var rating = dvd.rating;
        var notes = dvd.Notes;
        var dvdID = dvd.dvdId;

        var row = '<tr>';
            row += '<td>' + title + '</td>';
            row += '<td>' + releaseYear + '</td>';
            row += '<td>' + director + '</td>';
            row += '<td>' + rating + '</td>';
            //row += '<td><a onclick = "getDVDToEdit(' + dvdID + ')">Edit + "|"</a></td>'; // "|" '<td><a onclick = "deleteDVD(' + dvdID + ')">Delete</a></td>';
            //row += '<td><a onclick = "confirmDelete(' + dvdID + ')">Delete</a></td>';
            row += '<td><a onclick="getDVDToEdit('+ dvdID +')"> Edit</a>'+ " | " + '<a onclick="deleteDVD('+ dvdID +')"> Delete</a></td>';

            DVDdisplaytable.append(row);
        }); // end success:function
      },
      error: function() {
        //alert('failure');
      $('#errorMessages')
          .append($('<li>')
          .attr({class: 'list-group-item list-group-item-danger'})
          .text('Error calling web service. Please try again later.'));
      }

  }); //end ajax call
}

function checkAndDisplayValidationErrors_Edit(input){
  //clear displayed error messages if any
  $('#edit-dvd-error-messages').empty();

  //check for HTML5 validation errors and process/display appropriately
  //a place to hold error messages
  var errorMessages = [];

  //loop through ech input and check for validation errorMessages
  input.each(function(){
    //use HTML5 validation API to find val errorMessages
    if(!this.validity.valid)
    {
      var errorField = $('label[for='+this.id+']').text();
      //alert(errorField);
      if (errorField == 'Title:'){
        errorMessages.push(errorField + ' '+ "Please Enter a Title for the DVD");
      }
      else if(errorField == 'Release Year:') {
        errorMessages.push(errorField + ' '+ "Please Enter a 4-digit year");
      }

      else if(errorField == 'Director:') {
        errorMessages.push(errorField + ' '+ "Please Enter a Director for the DVD");
      }
    }

  });

  var parsedYear = parseInt($('#edit-release-year').val());
  if( parsedYear < 1000) {
  //if($('#add-release-year').val().parseInt)
  errorMessages.push('Release Year:' + ' '+ "The year must be 4 digits");
  alert(parsedYear);

    }

   //close of input.each

  //put any error messages in the errorMessages div - (appropriate one via div id)
  if(errorMessages.length > 0) {
    $.each(errorMessages, function(index, message){

      $('#edit-dvd-error-messages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });
      //return true, indicating that there were errorMessages
      return true;
    }

    else {
      //return false, indicating there were no errorMessages
      return false;
    }
}


function checkAndDisplayValidationErrors_Create(input) {
  //clear displayed error messages if any
  $('#create-dvd-error-messages').empty();

  //check for HTML5 validation errors and process/display appropriately
  //a place to hold error messages
  var errorMessages = [];

  //loop through ech input and check for validation errorMessages
  input.each(function(){
    //use HTML5 validation API to find val errorMessages
    if(!this.validity.valid)
    {
      var errorField = $('label[for='+this.id+']').text();
      //alert(errorField);
      if (errorField == 'Title:'){
        errorMessages.push(errorField + ' '+ "Please Enter a Title for the DVD");
      }
      else if(errorField == 'Release Year:') {
        errorMessages.push(errorField + ' '+ "Please Enter a 4-digit year");
      }

      else if(errorField == 'Director:') {
        errorMessages.push(errorField + ' '+ "Please Enter a Director for the DVD");
      }
    }

  });

  var parsedYear = parseInt($('#add-release-year').val());
  if( parsedYear < 1000) {
  //if($('#add-release-year').val().parseInt)
  errorMessages.push('Release Year:' + ' '+ "The year must be 4 digits");
  //alert(parsedYear);

    }

   //close of input.each

  //put any error messages in the errorMessages div - (appropriate one via div id)
  if(errorMessages.length > 0) {
    $.each(errorMessages, function(index, message){

      $('#create-dvd-error-messages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });
      //return true, indicating that there were errorMessages
      return true;
    }

    else {
      //return false, indicating there were no errorMessages
      return false;
    }

}


function checkAndDisplayValidationErrors_Search(input) {
  $('#errorMessages').empty();

  //check for HTML5 validation errors and process/display appropriately
  //a place to hold error messages
  var errorMessages = [];

  //loop through ech input and check for validation errorMessages
  //input.each(function(){
    //use HTML5 validation API to find val errorMessages
    // issue is that the below method is not evaluating to false
    //if(!input.validity.valid)
    if(true)
    {
      //var errorField = $('label[for='+this.id+']').text();
      alert('entered search errors');

      errorMessages.push(this.validationMessage);
      //if (errorField == 'Title:'){
      //  errorMessages.push(errorField + ' '+ "Please Enter a Title for the DVD");
      //}
      //else if(errorField == 'Release Year:') {
        //errorMessages.push(errorField + ' '+ "Please Enter a 4-digit year");
      //}

      //if(errorField == "") {
      //errorMessages.push(errorField + ' '+ "Please Enter a Director for the DVD");
    //}

    //  if(errorField == 'Director:') {
      //errorMessages.push(errorField + ' '+ "Please Enter a Director for the DVD");
    //}
    }

  //});
   //close of input.each
   //errorMessages.push('TEST ERROR');
  //put any error messages in the errorMessages div - (appropriate one via div id)
  if(errorMessages.length > 0) {
    $.each(errorMessages, function(index, message){

      $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
    });
      //return true, indicating that there were errorMessages
      return true;
    }

    else {
      //return false, indicating there were no errorMessages
      return false;
    }

}
