export const Utility ={
    
  alphabetsOnly(e) {
    var inputValue = e.which;
    if (!(inputValue >= 65 && inputValue <= 120) && (inputValue != 32 && inputValue != 0)) {
      e.preventDefault();
    }
  },
  
  numberOnly(e) {
    var inputValue = e.which;
    if (inputValue > 31 && (inputValue < 48 || inputValue > 57)) {
      e.preventDefault();
    }
  },

  alphaNumericOnly(e) {
    var specialKeys = new Array();
    specialKeys.push(8);  //Backspace
    specialKeys.push(9);  //Tab
    specialKeys.push(46); //Delete
    specialKeys.push(36); //Home
    specialKeys.push(35); //End
    specialKeys.push(37); //Left
    specialKeys.push(39); //Right
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    if((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || keyCode == 32 || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode)) {}
    else{
      e.preventDefault()
     }
  },

  getSubdomain() {
    const domain = window.location.hostname;
    const domainList = ['example', 'lvh', 'www'];
    const subDomainName = domain.split('.')[0];
    if (domain.indexOf('.') < 0 || domainList.includes(subDomainName)) {
      if(subDomainName === 'localhost')
      {
        return subDomainName;
      }
     return '';
    }
    return subDomainName;
  },

  validateDecimalValue(input, index){
    //decimal value upto index places
    var t = input.value;
    input.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), (index+1))) : t;
  },
  
  parseNumberString(numString: string): number | null {
    const parsedNumber = parseInt(numString);
    if (!isNaN(parsedNumber)) {
        return parsedNumber;
    }
    return null;
  },

  roundOffNumber(num: number, digitAfterDecimal:number): number {
    if (num % 1 === 0) { // Check if the number has no decimal places
        return num; // Return the number as it is
    } else {
        return parseFloat(num.toFixed(digitAfterDecimal)); // Return the rounded number with two decimal places
    }
  }
}