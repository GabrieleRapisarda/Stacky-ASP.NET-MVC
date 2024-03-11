var ListOftiny = document.querySelectorAll(".SliderBox");
var BlueButton = document.getElementById("nextShoe");

function swap() {
    console.log("Hai cliccato");
}

BlueButton.addEventListener("click", swap);
ListOftiny.forEach(e => e.addEventListener("click", swap));

