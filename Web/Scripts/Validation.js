// Created by Seeta 20073112

// this function checks the existing filed value for dot '.'Dot is accepted only once 
function allowKeyAcceptsSingleDot(acceptKeys)
{    
    
    var keyAscii,keyChar;
    keyAscii=window.event.keyCode;
    if(keyAscii==null)return false;
    keyChar=String.fromCharCode(keyAscii);
    keyChar=keyChar.toLowerCase();
    acceptKeys=acceptKeys.toLowerCase();   
    if(keyAscii==46)if(window.event.srcElement.value.indexOf(keyChar)!=-1)return false;
    if(acceptKeys.indexOf(keyChar) != -1)return true;
    return false;
}


//BY DINA: this function allow more than 1 dot 
function allowKeyAcceptsDotZ(acceptKeys)
{        
    var keyAscii,keyChar;
    keyAscii=window.event.keyCode;
    if(keyAscii==null)return false;
    keyChar=String.fromCharCode(keyAscii);
    keyChar=keyChar.toLowerCase();
    acceptKeys=acceptKeys.toLowerCase();   
    if(keyAscii==110)if(window.event.srcElement.value.indexOf(keyChar)!=-1)return false;
  if(acceptKeys.indexOf(keyChar) != -1)return true;
    return false;
}




function IsEmptyCheck(ctrlValue)
{
  if ((ctrlValue == 'null') || (ctrlValue == '') || (ctrlValue == '--Select--') || (ctrlValue == '0')) //28052008 dina
    {
        return false;
    }
    else
    {
        return true;
    }
}
//seeta 24072008
function allowKeyAcceptsData(acceptKeys)
{    
    var keyAscii,keyChar;
    keyAscii=window.event.keyCode;
    if(keyAscii==null)return false;
    keyChar=String.fromCharCode(keyAscii);
    keyChar=keyChar.toLowerCase();
    acceptKeys=acceptKeys.toLowerCase();    
    if(acceptKeys.indexOf(keyChar) != -1)return true;
    return false;
}


//try anuar > ddl empty check
function IsEmptyddlCheck(val)
{
    if((val == '0') || (val == '')||(val == null))
    {
        return false;
    }
    else
    {
        return true;
    } 
}

//try anuar> email validation
function validateEmail(email)
{
    // test if valid email address, must have @ and .
   var msg = '';
   var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
   //var address = document.forms[form_id].elements[email].value;
   var address = document.getElementById(email).value;
   if(reg.test(address) == false) {
      msg = 'Invalid Email Address'      
   }   
   return msg;   
}

//try anuar > phone validation

function validatePhone(phone) {   
   // only allow 0-9, hyphen and comma be entered
    var msg = '';
    var checkOK = "0123456789-,";
    var checkStr = document.getElementById(phone).value;
    var allValid = true;
    var decPoints = 0;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
         for (j = 0;  j < checkOK.length;  j++)
            if (ch == checkOK.charAt(j))
            break;
            if (j == checkOK.length)
            {
              allValid = false;
              break;
            }
        if (ch != ",")
        allNum += ch;
    }
    if (!allValid)
    {
         msg ="Invalid Contact Number format";
        //theForm.NumberText.focus();
        document.getElementById(phone).focus();
       
    }
    return msg;   
}

//try anuar>  number validation

function validateNumber(num) {   
   // only allow 0-9
    var msg = '';
    var checkOK = "0123456789";
    var checkStr = document.getElementById(num).value;
    var allValid = true;
    var decPoints = 0;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
         for (j = 0;  j < checkOK.length;  j++)
            if (ch == checkOK.charAt(j))
            break;
            if (j == checkOK.length)
            {
              allValid = false;
              break;
            }
        if (ch != ",")
        allNum += ch;
    }
    if (!allValid)
    {
         msg ="Invalid Number format";
        //theForm.NumberText.focus();
        document.getElementById(num).focus();
       
    }
    return msg;   
}

//try anuar> date validation format
//---------------------------------------------------------------------------------------------

// Declaring valid date character, minimum year and maximum year
var dtCh= "/";
var minYear=1900;
var maxYear=2500;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}
//where dtStr=document.getElementByID(....).value
function isDate(dtStr){
    var msg = '';
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		msg = "The date format should be : mm/dd/yyyy"
		
	}
	if (strMonth.length<1 || month<1 || month>12){
		msg ="Please enter a valid month"
		
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		msg ="Please enter a valid day"
	
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		msg ="Please enter a valid 4 digit year between "+minYear+" and "+maxYear 
		
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
	    msg = "Please enter a valid date"
		
	}
return msg 
}

//--------------------------------------------------------------------------------------------------------------------

/*function doNumberTest(frmname,ctrlValue){
    alert(document.forms(frmname).item(ctrlValue).value);
    var changeToNumberType = parseFloat(document.getElementById(ctrlValue).value);
    //var changeToNumberType =parseFloat(document.getElementById("ctrlValue").value);
	var mightBeNumber = isNaN(changeToNumberType);
	alert(mightBeNumber);
	if (mightBeNumber == false){
		alert("Accepts only numbers"); 
		ctrlValue.value='';
		ctrlValue.focus();
	} 
}*/







function Form1_Validator(theForm)
{

    var alertsay = ""; // define for long lines alertsay is not necessary for your code, 
    // but I need to break my lines in multiple lines so the code won't extend off the edge of the page
    // check to see if the field is blank
    if (theForm.Alias.value == "")
    {
        alert("You must enter an alias.");
        theForm.Alias.focus();
        return (false);
    }

    // require at least 3 characters be entered
    if (theForm.Alias.value.length < 3)
    {
    alert("Please enter at least 3 characters in the \"Alias\" field.");
    theForm.Alias.focus();
    return (false);
    }

    // allow ONLY alphanumeric keys, no symbols or punctuation this can be altered for any "checkOK" string you desire
    var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var checkStr = theForm.Alias.value;
    var allValid = true;
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkOK.length;  j++)
        if (ch == checkOK.charAt(j))
        break;
        if (j == checkOK.length)
        {
            allValid = false;
            break;
        }
    }
    if (!allValid)
    {
        alert("Please enter only letter and numeric characters in the \"Alias\" field.");
        theForm.Alias.focus();
        return (false);
    }

    // require at least 5 characters in the password field
    if (theForm.Password.value.length < 5)
    {
        alert("Please enter at least 5 characters in the \"Password\" field.");
        theForm.Password.focus();
        return (false);
    }

    // check if both password fields are the same
    if (theForm.Password.value != theForm.Password2.value)
    {
	    alert("The two passwords are not the same.");
	    theForm.Password2.focus();
	    return (false);
    }

    // allow only 150 characters maximum in the comment field
    if (theForm.comment.value.length > 150)
    {
        alert("Please enter at most 150 characters in the comment field.");
        theForm.comment.focus();
        return (false);
    }

    // check if no drop down has been selected
    if (theForm.sex.selectedIndex < 0)
    {
        alert("Please select one of the \"Gender\" options.");
        theForm.sex.focus();
        return (false);
    }

    // check if the first drop down is selected, if so, invalid selection
    if (theForm.sex.selectedIndex == 0)
    {
        alert("The first \"Gender\" option is not a valid selection.");
        theForm.sex.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_month.selectedIndex <= 0)
    {
        alert("Please select a month.");
        theForm.date_month.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_day.selectedIndex <= 0)
    {
        alert("Please select a day.");
        theForm.date_day.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_year.selectedIndex <= 0)
    {
        alert("Please select a year.");
        theForm.date_year.focus();
        return (false);
    }

    // check if email field is blank
    if (theForm.Email.value == "")
    {
        alert("Please enter a value for the \"Email\" field.");
        theForm.Email.focus();
        return (false);
    }


    // test if valid email address, must have @ and .
    var checkEmail = "@.";
    var checkStr = theForm.Email.value;
    var EmailValid = false;
    var EmailAt = false;
    var EmailPeriod = false;
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkEmail.length;  j++)
        {
            if (ch == checkEmail.charAt(j) && ch == "@")
            EmailAt = true;
            if (ch == checkEmail.charAt(j) && ch == ".")
            EmailPeriod = true;
	              if (EmailAt && EmailPeriod)
		            break;
	              if (j == checkEmail.length)
		            break;
        }
	    // if both the @ and . were in the string
        if (EmailAt && EmailPeriod)
        {
	        EmailValid = true
	        break;
        }
    }
    if (!EmailValid)
    {
        alert("The \"email\" field must contain an \"@\" and a \".\".");
        theForm.Email.focus();
        return (false);
    }


    // check if numbers field is blank
    if (theForm.numbers.value == "")
    {
        alert("Please enter a value for the \"numbers\" field.");
        theForm.numbers.focus();
        return (false);
    }

    // only allow numbers to be entered
    var checkOK = "0123456789";
    var checkStr = theForm.numbers.value;
    var allValid = true;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkOK.length;  j++)
        if (ch == checkOK.charAt(j))
        break;
        if (j == checkOK.length)
        {
            allValid = false;
            break;
        }
        if (ch != ",")
            allNum += ch;
    }
    if (!allValid)
    {
        alert("Please enter only digit characters in the \"numbers\" field.");
        theForm.numbers.focus();
        return (false);
    }

// require at least one radio button be selected
var radioSelected = false;
for (i = 0;  i < theForm.fruit.length;  i++)
{
if (theForm.fruit[i].checked)
radioSelected = true;
}
if (!radioSelected)
{
alert("Please select one of the \"Fruit\" options.");
return (false);
}

// check if no drop down or first drop down is selected, if so, invalid selection
if (theForm.rangefrom.selectedIndex <= 0)
{
alert("Please select a valid number in the range \"From\" field.");
theForm.rangefrom.focus();
return (false);
}

// check if no drop down or first drop down is selected, if so, invalid selection
if (theForm.rangeto.selectedIndex <= 0)
{
alert("Please select a valid number in the range \"To\" field.");
theForm.rangeto.focus();
return (false);
}

// require that the To Field be greater than or equal to the From Field
var chkVal = theForm.rangeto.value;
var chkVal2 = theForm.rangefrom.value;
if (chkVal != "" && !(chkVal >= chkVal2))
{
alert("The \"To\" value must be greater than or equal to (>=) the \"From\" value.");
theForm.rangeto.focus();
return (false);
}

// check if more than 5 options are selected
// check if less than 1 options are selected
var numSelected = 0;
var i;
for (i = 0;  i < theForm.province.length;  i++)
{
if (theForm.province.options[i].selected)
numSelected++;
}
if (numSelected > 5)
{
alert("Please select at most 5 of the \"province\" options.");
theForm.province.focus();
return (false);
}
if (numSelected < 1)
{
alert("Please select at least 1 of the \"province\" options.");
theForm.province.focus();
return (false);
}

// require a value be entered in the field
if (theForm.NumberText.value == "")
{
alert("Please enter a value for the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// require that at least one character be entered
if (theForm.NumberText.value.length < 1)
{
alert("Please enter at least 1 characters in the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// don't allow more than 5 characters be entered
if (theForm.NumberText.value.length > 5)
{
	 alertsay = "Please enter at most 5 characters in "
	 alertsay = alertsay + "the \"NumberText\" field, including comma."
alert(alertsay);
theForm.NumberText.focus();
return (false);
}

// only allow 0-9, hyphen and comma be entered
var checkOK = "0123456789-,";
var checkStr = theForm.NumberText.value;
var allValid = true;
var decPoints = 0;
var allNum = "";
for (i = 0;  i < checkStr.length;  i++)
{
ch = checkStr.charAt(i);
for (j = 0;  j < checkOK.length;  j++)
if (ch == checkOK.charAt(j))
break;
if (j == checkOK.length)
{
allValid = false;
break;
}
if (ch != ",")
allNum += ch;
}
if (!allValid)
{
alert("Please enter only digit characters in the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// require a minimum of 9 and a maximum of 5000
// allow 5,000 (with comma)
var chkVal = allNum;
var prsVal = parseInt(allNum);
if (chkVal != "" && !(prsVal >= "9" && prsVal <= "5000"))
{
	alertsay = "Please enter a value greater than or "
	alertsay = alertsay + "equal to \"9\" and less than or "
	alertsay = alertsay + "equal to \"5000\" in the \"NumberText\" field."
alert(alertsay);
theForm.NumberText.focus();
return (false);
}

// alert if the box is NOT checked
if (!theForm.checkbox1.checked)
{
alertsay = "Just reminding you that if you wish "
alertsay = alertsay + "to have our Super Duper option, "
alertsay = alertsay + "you must check the box!"
alert(alertsay);
}

// require that at least one checkbox be checked
var checkSelected = false;
for (i = 0;  i < theForm.checkbox2.length;  i++)
{
if (theForm.checkbox2[i].checked)
checkSelected = true;
}
if (!checkSelected)
{
alert("Please select at least one of the \"Test Checkbox\" options.");
return (false);
}

// only allow up to 2 checkboxes be checked
var checkCounter = 0;
for (i = 0;  i < theForm.checkbox2.length;  i++)
{
if (theForm.checkbox2[i].checked)
checkCounter = checkCounter + 1;
}
if (checkCounter > 2)
{
alert("Please select only one or two of the \"Test Checkbox\" options.");
return (false);
}

// because this is a sample page, don't allow to exit to the post action
// comes in handy when you are testing the form validations and don't
// wish to exit the page
alertsay = "All Validations have succeeded. "
alertsay = alertsay + "This is just a test page. There is no submission page."
alert(alertsay);
return (false);
// replace the above with return(true); if you have a valid form to submit to
}
//--></script>
/* Siva Script ************************************ */
// Created by Seeta 20073112

// this function checks the existing filed value for dot '.'Dot is accepted only once 
function allowKeyAcceptsSingleDot(acceptKeys)
{    
    var keyAscii,keyChar;
    keyAscii=window.event.keyCode;
    if(keyAscii==null)return false;
    keyChar=String.fromCharCode(keyAscii);
    keyChar=keyChar.toLowerCase();
    acceptKeys=acceptKeys.toLowerCase();
    if(keyAscii==46)if(window.event.srcElement.value.indexOf(keyChar)!=-1)return false;
    if(acceptKeys.indexOf(keyChar) != -1)return true;
    return false;
}

function IsEmptyCheck(ctrlValue)
{
    if ((ctrlValue == 'null') || (ctrlValue == '') || (ctrlValue == '--Select--'))
    {
        return false;
        alert("entered in to if");
    }
    else
    {
        return true;
        alert("entered in to else");
    }
}

function IsRadioEmpty(ctrlId)
{

var radCtrl = document.all.ctrlId;

var bReturn = false;
alert("entered in to the function");
for (var i = 0;i < radCtrl.length; i++)

{

if (radCtrl[i].checked == true)

{bReturn = true;}

}

if (bReturn == false){alert('You got to make a choice!');}

return bReturn;
}



// Siva Check List Validation
function IsCheckedList(ctrlId)
{
var options = document.getElementById(ctrlId).getElementsByTagName('input');        
        var ischecked=false;
        alert(options.length);
        for(i=0;i<options.length;i++) 
        { 
             
               var opt = options[i];
                   if(opt.checked) 
                   {
                    return true;
                   }
                   else
                   {                  
                    return false;                                       
                   }
        }
}

function IsCheckedRadList(ctrlId)
{
alert("entered in to the function");  
var options = document.all.ctrlId; 
      alert(options.length);       
        var ischecked=false;
        
        for(i=0;i<options.length;i++) 
        {              
               var opt = options[i]; 
               
                   if(opt.checked) 
                   {
                    return true;
                   }
                   else
                   {                  
                    return false;                                       
                   }               
        }
}


//try anuar > ddl empty check
function IsEmptyddlCheck(val)
{
    if((val == '0') || (val == '')||(val == null))
    {
        return false;
    }
    else
    {
        return true;
    } 
}

//try anuar> email validation
function validateEmail(email)
{
    // test if valid email address, must have @ and .
   var msg = '';
   var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
   //var address = document.forms[form_id].elements[email].value;
   var address = document.getElementById(email).value;
   if(reg.test(address) == false) {
      msg = 'Invalid Email Address'      
   }   
   return msg;   
}

//try anuar > phone validation

function validatePhone(phone) {   
   // only allow 0-9, hyphen and comma be entered
    var msg = '';
    var checkOK = "0123456789-,";
    var checkStr = document.getElementById(phone).value;
    var allValid = true;
    var decPoints = 0;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
         for (j = 0;  j < checkOK.length;  j++)
            if (ch == checkOK.charAt(j))
            break;
            if (j == checkOK.length)
            {
              allValid = false;
              break;
            }
        if (ch != ",")
        allNum += ch;
    }
    if (!allValid)
    {
         msg ="Invalid Contact Number format";
        //theForm.NumberText.focus();
        document.getElementById(phone).focus();
       
    }
    return msg;   
}

//try anuar>  number validation

function validateNumber(num) {   
   // only allow 0-9
    var msg = '';
    var checkOK = "0123456789";
    var checkStr = document.getElementById(num).value;
    var allValid = true;
    var decPoints = 0;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
         for (j = 0;  j < checkOK.length;  j++)
            if (ch == checkOK.charAt(j))
            break;
            if (j == checkOK.length)
            {
              allValid = false;
              break;
            }
        if (ch != ",")
        allNum += ch;
    }
    if (!allValid)
    {
         msg ="Invalid Number format";
        //theForm.NumberText.focus();
        document.getElementById(num).focus();
       
    }
    return msg;   
}

//try anuar> date validation format
//---------------------------------------------------------------------------------------------

// Declaring valid date character, minimum year and maximum year
var dtCh= "/";
var minYear=1900;
var maxYear=2500;

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}
//where dtStr=document.getElementByID(....).value
function isDate(dtStr){
    var msg = '';
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		msg = "The date format should be : mm/dd/yyyy"
		
	}
	if (strMonth.length<1 || month<1 || month>12){
		msg ="Please enter a valid month"
		
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		msg ="Please enter a valid day"
	
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear){
		msg ="Please enter a valid 4 digit year between "+minYear+" and "+maxYear 
		
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false){
	    msg = "Please enter a valid date"
		
	}
return msg 
}

//--------------------------------------------------------------------------------------------------------------------

/*function doNumberTest(frmname,ctrlValue){
    alert(document.forms(frmname).item(ctrlValue).value);
    var changeToNumberType = parseFloat(document.getElementById(ctrlValue).value);
    //var changeToNumberType =parseFloat(document.getElementById("ctrlValue").value);
	var mightBeNumber = isNaN(changeToNumberType);
	alert(mightBeNumber);
	if (mightBeNumber == false){
		alert("Accepts only numbers"); 
		ctrlValue.value='';
		ctrlValue.focus();
	} 
}*/

function Form1_Validator(theForm)
{

    var alertsay = ""; // define for long lines alertsay is not necessary for your code, 
    // but I need to break my lines in multiple lines so the code won't extend off the edge of the page
    // check to see if the field is blank
    if (theForm.Alias.value == "")
    {
        alert("You must enter an alias.");
        theForm.Alias.focus();
        return (false);
    }

    // require at least 3 characters be entered
    if (theForm.Alias.value.length < 3)
    {
    alert("Please enter at least 3 characters in the \"Alias\" field.");
    theForm.Alias.focus();
    return (false);
    }

    // allow ONLY alphanumeric keys, no symbols or punctuation this can be altered for any "checkOK" string you desire
    var checkOK = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var checkStr = theForm.Alias.value;
    var allValid = true;
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkOK.length;  j++)
        if (ch == checkOK.charAt(j))
        break;
        if (j == checkOK.length)
        {
            allValid = false;
            break;
        }
    }
    if (!allValid)
    {
        alert("Please enter only letter and numeric characters in the \"Alias\" field.");
        theForm.Alias.focus();
        return (false);
    }

    // require at least 5 characters in the password field
    if (theForm.Password.value.length < 5)
    {
        alert("Please enter at least 5 characters in the \"Password\" field.");
        theForm.Password.focus();
        return (false);
    }

    // check if both password fields are the same
    if (theForm.Password.value != theForm.Password2.value)
    {
	    alert("The two passwords are not the same.");
	    theForm.Password2.focus();
	    return (false);
    }

    // allow only 150 characters maximum in the comment field
    if (theForm.comment.value.length > 150)
    {
        alert("Please enter at most 150 characters in the comment field.");
        theForm.comment.focus();
        return (false);
    }

    // check if no drop down has been selected
    if (theForm.sex.selectedIndex < 0)
    {
        alert("Please select one of the \"Gender\" options.");
        theForm.sex.focus();
        return (false);
    }

    // check if the first drop down is selected, if so, invalid selection
    if (theForm.sex.selectedIndex == 0)
    {
        alert("The first \"Gender\" option is not a valid selection.");
        theForm.sex.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_month.selectedIndex <= 0)
    {
        alert("Please select a month.");
        theForm.date_month.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_day.selectedIndex <= 0)
    {
        alert("Please select a day.");
        theForm.date_day.focus();
        return (false);
    }

    // check if no drop down or first drop down is selected, if so, invalid selection
    if (theForm.date_year.selectedIndex <= 0)
    {
        alert("Please select a year.");
        theForm.date_year.focus();
        return (false);
    }

    // check if email field is blank
    if (theForm.Email.value == "")
    {
        alert("Please enter a value for the \"Email\" field.");
        theForm.Email.focus();
        return (false);
    }


    // test if valid email address, must have @ and .
    var checkEmail = "@.";
    var checkStr = theForm.Email.value;
    var EmailValid = false;
    var EmailAt = false;
    var EmailPeriod = false;
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkEmail.length;  j++)
        {
            if (ch == checkEmail.charAt(j) && ch == "@")
            EmailAt = true;
            if (ch == checkEmail.charAt(j) && ch == ".")
            EmailPeriod = true;
	              if (EmailAt && EmailPeriod)
		            break;
	              if (j == checkEmail.length)
		            break;
        }
	    // if both the @ and . were in the string
        if (EmailAt && EmailPeriod)
        {
	        EmailValid = true
	        break;
        }
    }
    if (!EmailValid)
    {
        alert("The \"email\" field must contain an \"@\" and a \".\".");
        theForm.Email.focus();
        return (false);
    }


    // check if numbers field is blank
    if (theForm.numbers.value == "")
    {
        alert("Please enter a value for the \"numbers\" field.");
        theForm.numbers.focus();
        return (false);
    }

    // only allow numbers to be entered
    var checkOK = "0123456789";
    var checkStr = theForm.numbers.value;
    var allValid = true;
    var allNum = "";
    for (i = 0;  i < checkStr.length;  i++)
    {
        ch = checkStr.charAt(i);
        for (j = 0;  j < checkOK.length;  j++)
        if (ch == checkOK.charAt(j))
        break;
        if (j == checkOK.length)
        {
            allValid = false;
            break;
        }
        if (ch != ",")
            allNum += ch;
    }
    if (!allValid)
    {
        alert("Please enter only digit characters in the \"numbers\" field.");
        theForm.numbers.focus();
        return (false);
    }

// require at least one radio button be selected
var radioSelected = false;
for (i = 0;  i < theForm.fruit.length;  i++)
{
if (theForm.fruit[i].checked)
radioSelected = true;
}
if (!radioSelected)
{
alert("Please select one of the \"Fruit\" options.");
return (false);
}

// check if no drop down or first drop down is selected, if so, invalid selection
if (theForm.rangefrom.selectedIndex <= 0)
{
alert("Please select a valid number in the range \"From\" field.");
theForm.rangefrom.focus();
return (false);
}

// check if no drop down or first drop down is selected, if so, invalid selection
if (theForm.rangeto.selectedIndex <= 0)
{
alert("Please select a valid number in the range \"To\" field.");
theForm.rangeto.focus();
return (false);
}

// require that the To Field be greater than or equal to the From Field
var chkVal = theForm.rangeto.value;
var chkVal2 = theForm.rangefrom.value;
if (chkVal != "" && !(chkVal >= chkVal2))
{
alert("The \"To\" value must be greater than or equal to (>=) the \"From\" value.");
theForm.rangeto.focus();
return (false);
}

// check if more than 5 options are selected
// check if less than 1 options are selected
var numSelected = 0;
var i;
for (i = 0;  i < theForm.province.length;  i++)
{
if (theForm.province.options[i].selected)
numSelected++;
}
if (numSelected > 5)
{
alert("Please select at most 5 of the \"province\" options.");
theForm.province.focus();
return (false);
}
if (numSelected < 1)
{
alert("Please select at least 1 of the \"province\" options.");
theForm.province.focus();
return (false);
}

// require a value be entered in the field
if (theForm.NumberText.value == "")
{
alert("Please enter a value for the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// require that at least one character be entered
if (theForm.NumberText.value.length < 1)
{
alert("Please enter at least 1 characters in the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// don't allow more than 5 characters be entered
if (theForm.NumberText.value.length > 5)
{
	 alertsay = "Please enter at most 5 characters in "
	 alertsay = alertsay + "the \"NumberText\" field, including comma."
alert(alertsay);
theForm.NumberText.focus();
return (false);
}

// only allow 0-9, hyphen and comma be entered
var checkOK = "0123456789-,";
var checkStr = theForm.NumberText.value;
var allValid = true;
var decPoints = 0;
var allNum = "";
for (i = 0;  i < checkStr.length;  i++)
{
ch = checkStr.charAt(i);
for (j = 0;  j < checkOK.length;  j++)
if (ch == checkOK.charAt(j))
break;
if (j == checkOK.length)
{
allValid = false;
break;
}
if (ch != ",")
allNum += ch;
}
if (!allValid)
{
alert("Please enter only digit characters in the \"NumberText\" field.");
theForm.NumberText.focus();
return (false);
}

// require a minimum of 9 and a maximum of 5000
// allow 5,000 (with comma)
var chkVal = allNum;
var prsVal = parseInt(allNum);
if (chkVal != "" && !(prsVal >= "9" && prsVal <= "5000"))
{
	alertsay = "Please enter a value greater than or "
	alertsay = alertsay + "equal to \"9\" and less than or "
	alertsay = alertsay + "equal to \"5000\" in the \"NumberText\" field."
alert(alertsay);
theForm.NumberText.focus();
return (false);
}

// alert if the box is NOT checked
if (!theForm.checkbox1.checked)
{
alertsay = "Just reminding you that if you wish "
alertsay = alertsay + "to have our Super Duper option, "
alertsay = alertsay + "you must check the box!"
alert(alertsay);
}

// require that at least one checkbox be checked
var checkSelected = false;
for (i = 0;  i < theForm.checkbox2.length;  i++)
{
if (theForm.checkbox2[i].checked)
checkSelected = true;
}
if (!checkSelected)
{
alert("Please select at least one of the \"Test Checkbox\" options.");
return (false);
}

// only allow up to 2 checkboxes be checked
var checkCounter = 0;
for (i = 0;  i < theForm.checkbox2.length;  i++)
{
if (theForm.checkbox2[i].checked)
checkCounter = checkCounter + 1;
}
if (checkCounter > 2)
{
alert("Please select only one or two of the \"Test Checkbox\" options.");
return (false);
}

// because this is a sample page, don't allow to exit to the post action
// comes in handy when you are testing the form validations and don't
// wish to exit the page
alertsay = "All Validations have succeeded. "
alertsay = alertsay + "This is just a test page. There is no submission page."
alert(alertsay);
return (false);
// replace the above with return(true); if you have a valid form to submit to
}
//--></script>




