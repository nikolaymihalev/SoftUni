function SetLimit() {
    let num = document.getElementById("limitInput").value || 50;

    window.location = "https://localhost:7267/Numbers/Limit?numb=" + num;
}