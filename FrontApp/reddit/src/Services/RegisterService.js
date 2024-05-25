import axios from 'axios';

export function DropErrorForField(
    firstNameError,
    lastNameError,
    addressError,
    cityError,
    emailError,
    countryError,
    passwordError,
    phoneNumError,
    imageUrlError
) {
    let isValid = true;

    if (firstNameError) {
        alert("First Name is required");
        isValid = false;
    }
    if (lastNameError) {
        alert("Last Name is required");
        isValid = false;
    }
    if (cityError) {
        alert("City is required");
        isValid = false;
    }
    if (addressError) {
        alert("Address is required");
        isValid = false;
    }
    if (countryError) {
        alert("Country is required");
        isValid = false;
    }
    if (emailError) {
        alert("This is not a valid email!");
        isValid = false;
    }
    if (passwordError) {
        alert("Password is required!");
        isValid = false;
    }
    if (imageUrlError) {
        alert("Image is required");
        isValid = false;
    }
    if (phoneNumError) {
        alert("Phone number is required!");
        isValid = false;
    }

    return isValid;
}

export default async function RegisterApiCall(
    firstNameError,
    lastNameError,
    cityError,
    addressError,
    countryError,
    emailError,
    passwordError,
    phoneNumError,
    imageUrlError,
    firstName,
    lastName,
    city,
    address,
    country,
    email,
    password,
    imageUrl,
    phoneNum,
    apiEndpoint
) {
    const isCompleted = DropErrorForField(
        firstNameError,
        lastNameError,
        addressError,
        cityError,
        emailError,
        countryError,
        passwordError,
        phoneNumError,
        imageUrlError,
    );
    console.log(imageUrl);
    if (isCompleted) {
        const userData = new FormData();
        userData.append('FirstName', firstName);
        userData.append('LastName', lastName);
        userData.append('City', city);
        userData.append('Address', address);
        userData.append('Country', country);
        userData.append('Email', email);
        userData.append('Password', password);
        userData.append('PhoneNum', phoneNum);
        userData.append('Image', imageUrl);
        
        console.log(typeof imageUrl);
        console.log(imageUrl);
        try {
            const response = await axios.post(apiEndpoint, userData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            console.log('Success register of user:', response.data);
            return response.data;
        } catch (error) {
            console.error('API call error:', error);
        }

       
    }
}
