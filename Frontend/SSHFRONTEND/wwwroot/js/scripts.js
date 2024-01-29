// scripts.js

function showVirtualMachineInfo() {
    var select = document.getElementById("virtualMachineSelect");
    var selectedVirtualMachine = select.options[select.selectedIndex].value;

    document.getElementById("loadingMessage").innerText = "Veriler alınıyor...";
    document.getElementById("loadingMessage").style.display = "block";

    // AJAX isteği yaparak sanal makina durumunu kontrol et
    fetch(`http://localhost:5134/api/SSHWebAPI/${selectedVirtualMachine}`, {
        method: 'GET',
        mode: 'cors',
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.text(); // Veriyi text olarak al
        })
        .then(data => {
            const jsonData = JSON.parse(data);

            // Geri kalan kodları buraya taşı
            console.log(jsonData);

            document.getElementById("virtualMachineInfo").style.display = "block";
            var statusRectangleElement = document.getElementById("statusRectangle");
            statusRectangleElement.style.backgroundColor = jsonData.isRunning ? "green" : "red";

            var virtualMachineNameElement = document.getElementById("virtualMachineName");
            virtualMachineNameElement.innerText = "Seçilen Sanal Makina: " + selectedVirtualMachine;
            document.getElementById("loadingMessage").style.display = "none";
        })
        .catch(error => {
            console.error('Hata:', error);
            document.getElementById("loadingMessage").style.display = "none";
        });
}

function toggleVirtualMachineStatus() {
    var select = document.getElementById("virtualMachineSelect");
    var selectedVirtualMachine = select.options[select.selectedIndex].value;

    document.getElementById("loadingMessage").innerText = "İşlem yapılıyor...";
    document.getElementById("loadingMessage").style.display = "block";

    // AJAX isteği yaparak sanal makina durumunu kontrol et ve değiştir
    fetch(`http://localhost:5134/api/SSHWebAPI/${selectedVirtualMachine}/toggle`,
        {
            method: 'POST',
            mode: 'cors',
        })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.text(); // Veriyi text olarak al
        })
        .then(data => {
            // JSON parse işlemini burada yap
            const jsonData = JSON.parse(data);

            // Durum kutusunu güncelle
            var statusRectangleElement = document.getElementById("statusRectangle");
            if (jsonData.isRunning) {
                statusRectangleElement.classList.remove("red");
                statusRectangleElement.classList.add("green");
            } else {
                statusRectangleElement.classList.remove("green");
                statusRectangleElement.classList.add("red");
            }

            document.getElementById("loadingMessage").innerText = `${jsonData.message}`;
            document.getElementById("loadingMessage").style.display = "block";
        })
        .catch(error => {
            console.error('Hata:', error);
            document.getElementById("loadingMessage").style.display = "none";
        });
}

function DockerStatus() {
    var select = document.getElementById("virtualMachineSelect");
    var selectedVirtualMachine = select.options[select.selectedIndex].value;

    document.getElementById("loadingMessage").innerText = "Veriler alınıyor...";
    document.getElementById("loadingMessage").style.display = "block";

    // AJAX isteği yaparak Docker durumunu kontrol et
    fetch(`http://localhost:5134/api/SSHWebAPI/docker/${selectedVirtualMachine}`, {
        method: 'GET',
        mode: 'cors',
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            // Durum kutusunu güncelle
            var dockerStatusCircleElement = document.getElementById("dockerStatusCircle");

            // data içinde message adında bir özellik varsa kontrol et
            if (data.message && data.message.includes("kurulu")) {
                dockerStatusCircleElement.style.backgroundColor = "green";
            } else {
                dockerStatusCircleElement.style.backgroundColor = "red";
            }

            document.getElementById("loadingMessage").innerText = "";
            document.getElementById("loadingMessage").style.display = "none";

            // Docker Durumu bölümünü göster
            document.getElementById("dockerStatusInfo").style.display = "block";
        })
        .catch(error => {
            console.error('Hata:', error);
            document.getElementById("loadingMessage").innerText = "Bir hata oluştu. Docker durumu alınamadı.";
            document.getElementById("loadingMessage").style.display = "block";
        });
}




