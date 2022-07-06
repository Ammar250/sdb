var baseApplicationPath = null;
var userName = null;
var userPass = null;
var confirmPass = null;
var userAddress = null;
var userEmail = null;
var userContact = null;
var regualrExp = null;
var isValidUserName = false, isValidUserPass = false;
var isConfirmPass = false, isValidUserAddress = false;
var isValidUserEmail = false, isValidUserCntct = false;
let loginUserEmail = null;
let loginUserPwd = null;
let isLoginUserEmailValid = false, isLoginUserPwdValid = false;

function showRegisterForm() {
    $('.loginBox').fadeOut('fast',function(){
        $('.registerBox').fadeIn('fast');
        $('.login-footer').fadeOut('fast',function(){
            $('.register-footer').fadeIn('fast');
        });
        $('.modal-title').html('Register Form');
    }); 
    $('.error').removeClass('alert alert-danger').html('');
       
}
function showLoginForm(){
    $('#loginModal .registerBox').fadeOut('fast',function(){
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast',function(){
            $('.login-footer').fadeIn('fast');    
        });
        
        $('.modal-title').html('Login Form');
    });       
     $('.error').removeClass('alert alert-danger').html(''); 
}

function openLoginModal(){
    showLoginForm();
    setTimeout(function(){
        $('#loginModal').modal('show');    
    }, 230);
    
}
function openRegisterModal(){
    showRegisterForm();
    setTimeout(function(){
        $('#loginModal').modal('show');    
    }, 230);
    
}

function setBaseUrl(baseUrl) {
    baseApplicationPath = baseUrl;
    console.log(baseApplicationPath);
}

function loginAjax() {

    //console.log("calling loginAjax");
    if (!isLoginUserEmailValid) {
        loginUserEmail = $("#email").val();
        regualrExp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        isLoginUserEmailValid = checkRegularExpressionControl(loginUserEmail, "#emailSpan", regualrExp, "E-mail format Must Be (abc@abc.com) ");
    }

    if (!isLoginUserPwdValid) {
        loginUserPwd = $("#password").val();
        regualrExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$/;
        isLoginUserPwdValid = checkRegularExpressionControl(loginUserPwd, "#passwordSpan", regualrExp, "Password Must have One LowerCae,One UpperCase and digits and Range (8-15)");
    }

    if (isLoginUserEmailValid && isLoginUserPwdValid) {
       // console.log("calling controller class action method " + loginUserEmail + " " + loginUserPwd);
        $.ajax({
            type: "Post",
            url: "/Login/Get",
            data: { Email: loginUserEmail, Password: loginUserPwd },
            success: function (data) {
                console.log(data);
                if (data == "NGO") {
                    $(location).prop('href', "/Ngo/Home/Index");
                }
                else if (data == "DONOR") {
                    $(location).prop('href', "/Donor/Home/Index");
                } else if (data == "ADMIN") {
                    $(location).prop('href', "/Admin/Home/Index");
                }else {
                    $("#lblloginError").css("display", "block");
                    $("#lblloginError").html(data).show().fadeOut(20000);
                }

            },
            error: function (data) {
                console.log(data);
                $("#lblloginError").css("display", "block");
                $("#lblloginError").html(data.responseText).show().fadeOut(20000);
            }

        });
    } else {
        $("#lblloginError").css("display", "block");
        $("#lblloginError").html("Please Enter User email and Password").show().fadeOut(20000);
    }
}


function checkRegularExpressionControl(controlValue, controlErrorID, controlRegex, message) {

    if (controlValue.length === 0) {

        $(controlErrorID).html("*").show().fadeOut(20000);
        return false;
    }
    else {
        //alert(controlValue);
        var regualrExp = controlRegex;
        if (!regualrExp.test(controlValue)) {

            $(controlErrorID).html(message).show().fadeOut(20000);
            return false;
        }

    }
    return true;
}
function checkRangeControl(controlValue, controlErrorID, minValue, maxValue) {

    if (controlValue.length === 0) {

        $(controlErrorID).html("*").show().fadeOut(20000);
        return false;
    }
    else {

        if (!(controlValue >= minValue && controlValue <= maxValue)) {

            $(controlErrorID).html("Value Must Be( " + minValue + "-" + maxValue + ")").show().fadeOut(20000);
            return false;
        }
    }
    return true;
}
$(document).ready(function () {
    //alert("asad");
    $("#lblError").hide();
    $("#lblError").css("display", "none");
    //  event.preventDefault();

    $("#txtName").focusout(function () {

        userName = $("#txtName").val();
        regualrExp = /^[a-zA-Z](_(?!(\.|_))|\.(?!(_|\.))|( )|[a-zA-Z]){1,50}[a-zA-Z]$/;
        isValidUserName = checkRegularExpressionControl(userName, "#userNameSpan", regualrExp, "User Name Must Be Start With Letter and (3-50) characters");

    });

    $("#txtPassword").focusout(function () {

        userPass = $("#txtPassword").val();
        regualrExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$/;
        isValidUserPass = checkRegularExpressionControl(userPass, "#registerpasswordSpan", regualrExp, "Password Must have One LowerCae,One UpperCase and digits and Range (8-15)");
    });

    $("#txt_password_confirmation").focusout(function () {

        confirmPass = $("#txt_password_confirmation").val();
        console.log(userPass + "," + confirmPass);
        if (!(userPass === confirmPass)) {

            $("#confirmPassSpan").html("Re-Type Password").show().fadeOut(20000);
        }
        else {
            isConfirmPass = true;
        }

    });

    $("#txtAddress").focusout(function () {

        userAddress = $("#txtAddress").val();
        regualrExp = /^[a-zA-Z]{3,50}$/;
        isValidUserAddress = checkRegularExpressionControl(userAddress, "#userAddressSpan", regualrExp, "Please Type Only Letters  Between (3-50) Letters");
    });

    $("#txtEmail").focusout(function () {

        userEmail = $("#txtEmail").val();
        regualrExp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        isValidUserEmail = checkRegularExpressionControl(userEmail, "#emailSpan", regualrExp, "E-mail format Must Be (abc@abc.com) ");
    });

    $("#txtPhone").focusout(function () {

        userContact = $("#txtPhone").val();
        regualrExp = /^\+?[0-9][0-9\s.-]{7,12}$/;
        isValidUserCntct = checkRegularExpressionControl(userContact, "#contactSpan", regualrExp, "Contact Number Must Be Contain Digits");
    });

    $("#rbtnDonor").click(function () {

        var selected = $("#rbtnNgo").is(':checked');
        if (selected) {
            $("#rbtnNgo").prop('checked', false);
        }

    });

    $("#rbtnNgo").click(function () {

        var selected = $("#rbtnDonor").is(':checked');
        if (selected) {
            $("#rbtnDonor").prop('checked', false);
        }

    });


    // Login fields Validations
    $("#email").focusout(function () {

        loginUserEmail = $("#email").val();
        regualrExp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        isLoginUserEmailValid = checkRegularExpressionControl(loginUserEmail, "#emailSpan", regualrExp, "E-mail format Must Be (abc@abc.com) ");
    });

    $("#password").focusout(function () {

        loginUserPwd = $("#password").val();
        regualrExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$/;
        isLoginUserPwdValid = checkRegularExpressionControl(loginUserPwd, "#passwordSpan", regualrExp, "Password Must have One LowerCae,One UpperCase and digits and Range (8-15)");
    });
});

function registerAjax() {
    console.log(isValidUserName + "," + isValidUserAddress + "," + isValidUserCntct + "," + isValidUserEmail + "," + isValidUserPass + "," + isConfirmPass );
    if (!isValidUserName) {
        userName = $("#txtName").val();
        regualrExp = /^[a-zA-Z](_(?!(\.|_))|\.(?!(_|\.))|( )|[a-zA-Z]){1,50}[a-zA-Z]$/;
        isValidUserName = checkRegularExpressionControl(userName, "#userNameSpan", regualrExp, "User Name Must Be Start With Letter and (3-50) characters");
    } else if (!isValidUserAddress) {
        userAddress = $("#txtAddress").val();
        regualrExp = /^[a-zA-Z]{3,50}$/;
        isValidUserAddress = checkRegularExpressionControl(userAddress, "#userAddressSpan", regualrExp, "Please Type Only Letters  Between (3-50) Letters");
    } else if (!isValidUserCntct) {
        userContact = $("#txtPhone").val();
        regualrExp = /^\+?[0-9][0-9\s.-]{7,12}$/;
        isValidUserCntct = checkRegularExpressionControl(userContact, "#contactSpan", regualrExp, "Contact Number Must Be Contain Digits");
    } else if (!isValidUserEmail) {
        userEmail = $("#txtEmail").val();
        regualrExp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
        isValidUserEmail = checkRegularExpressionControl(userEmail, "#emailSpan", regualrExp, "E-mail format Must Be (abc@abc.com) ");
    } else if (!isValidUserPass) {
        userPass = $("#txtPassword").val();
        regualrExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$/;
        isValidUserPass = checkRegularExpressionControl(userPass, "#registerpasswordSpan", regualrExp, "Password Must have One LowerCae,One UpperCase and digits and Range (8-15)");
    } else if (!isConfirmPass) {
        $("#confirmPassSpan").html("Re-Type Password").show().fadeOut(20000);
    }

    var userRole = 'DEFAULT';
    //e.preventDefault();
    //alert("Hello");
    if ($("#rbtnDonor").is(':checked')) {
        userRole = 'DONOR';
    }
    else if ($("#rbtnNgo").is(':checked')) {
        userRole = 'NGO';
    }
    else {
        //alert("Please Select User Role");
        $("#lblError").css("display", "block");
        $("#lblError").html("Please Select User Role");
        return;
    }

    if (isValidUserName && isValidUserPass && isConfirmPass && isValidUserAddress
        && isValidUserEmail && isValidUserCntct && (userRole != 'DEFAULT')) {
        //console.log(userName + "," + userAddress + "," + userContact + "," + userEmail + "," + userPass + "," + confirmPass);
        //alert("Valid");
        $.ajax({

            type: "Post",
            url: "/UserRegister/Create",
            data: { Name: userName, Password: userPass, Address: userAddress, userRole: userRole, Email: userEmail, Phone: userContact },
            success: function (data) {
                console.log(data);
                if (data == "You have successfully registered yourself\nNow Please try login") {

                    //alert("Save Successfully");
                    //$(location).prop('href', "/Welcome_Survey_App_Page.aspx");
                    openLoginModal();
                    
                    $("#lblloginError").css("display", "block");
                    $("#lblloginError").html(data).show().fadeOut(20000);
                } else {
                    $("#lblError").css("display", "block");
                    $("#lblError").html(data).show().fadeOut(20000);
                }
                
            },

            error: function (data) {
                //alert(data.d);
                console.log(data);
                $("#lblError").css("display", "block");
                $("#lblError").html(data.responseText).show().fadeOut(20000);
            }

        });
    }
    else {

        //alert("Please Fill All Input Fields");
        $("#lblError").css("display", "block");
        $("#lblError").html("Please Fill All Input Fields");
        return;
    }

}
   