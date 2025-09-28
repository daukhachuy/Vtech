
//// Profile page functionality
//document.addEventListener("DOMContentLoaded", () => {
//    updateUserInfo()
//})

//function updateUserInfo() {
//    const userName = localStorage.getItem("userName") || "John Doe"
//    const userEmail = localStorage.getItem("userEmail") || "john.doe@email.com"

//    document.getElementById("userName").textContent = userName
//    document.getElementById("profileName").textContent = userName
//    document.getElementById("profileEmail").textContent = userEmail
//}

function editPersonalInfo() {
    const modal = new window.bootstrap.Modal(document.getElementById("editPersonalModal"))

    modal.show()
}

//function savePersonalInfo() {
//    const firstName = document.getElementById("editFirstName").value
//    const lastName = document.getElementById("editLastName").value
//    const email = document.getElementById("editEmail").value
//    const phone = document.getElementById("editPhone").value
//    const dob = document.getElementById("editDOB").value

//    // Update localStorage
//    localStorage.setItem("userName", `${firstName} ${lastName}`)
//    localStorage.setItem("userEmail", email)

//    // Update UI
//    updateUserInfo()
//    document.getElementById("profilePhone").textContent = phone
//    document.getElementById("profileDOB").textContent = new Date(dob).toLocaleDateString("en-US", {
//        year: "numeric",
//        month: "long",
//        day: "numeric",
//    })

//    // Close modal
//    const modal = window.bootstrap.Modal.getInstance(document.getElementById("editPersonalModal"))
//    modal.hide()

//    alert("Personal information updated successfully!")
//}

//function editHealthInfo() {
//    alert("Health information editing would be implemented here")
//}

//function editInsurance() {
//    alert("Insurance information editing would be implemented here")
//}

//function changeAvatar() {
//    alert("Avatar change functionality would be implemented here")
//}

//function addAllergy() {
//    const allergy = prompt("Enter new allergy:")
//    if (allergy) {
//        alert(`Added allergy: ${allergy}`)
//    }
//}

//function addMedication() {
//    const medication = prompt("Enter new medication:")
//    if (medication) {
//        alert(`Added medication: ${medication}`)
//    }
//}

//function addCondition() {
//    const condition = prompt("Enter new medical condition:")
//    if (condition) {
//        alert(`Added condition: ${condition}`)
//    }
//}

//function deleteAccount() {
//    if (confirm("Are you sure you want to delete your account? This action cannot be undone.")) {
//        if (confirm("This will permanently delete all your data. Are you absolutely sure?")) {
//            alert("Account deletion would be processed here")
//        }
//    }
//}

function logout() {
    if (confirm("Bạn chắc chắn muốn thoát khỏi hệ thống?")) {
        localStorage.clear()
        window.location.href = '/Account/Logout';
    }
}



