// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.17.1/firebase-app.js";
import { getStorage, ref, listAll, getDownloadURL, uploadBytes } from "https://www.gstatic.com/firebasejs/9.17.1/firebase-storage.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyBXtrCKY7bnByz6gOVzZrum_5FZJqYG__k",
    authDomain: "minkyshop-bed92.firebaseapp.com",
    projectId: "minkyshop-bed92",
    storageBucket: "minkyshop-bed92.appspot.com",
    messagingSenderId: "340483115748",
    appId: "1:340483115748:web:62f36125d56e42cdcf3af1",
    measurementId: "G-4MPKL5WTFD"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

const storage = getStorage();

// Create a reference under which you want to list
const listRef = ref(storage, 'images/');

async function storageImages() {
    var res = await listAll(listRef)
    var itemRef = await res.items;
    var images = [];
    for (var x of itemRef) {
        images.push(await getDownloadURL(x))
    }
    return images;
}

async function uploadImages(images) {
    //for (var image of images) {
    //    //uploadBytes(storageRef, new File([image], image.name, { type: image.contentType })).then((snapshot) => {
    //    //    console.log('Uploaded a blob or file!');
    //    //});
    //    console.log(new File([image], image.name, { type: image.contentType }));
    //}
    const storageRef = ref(storage, 'images/' + "123");
    uploadBytes(storageRef, images).then((snapshot) => {
        console.log('Uploaded a blob or file!');
    });
}

function choiceLoad(obj, classname) {
    var element = classname;
    if (element) new Choices(document.querySelector(element), JSON.parse(obj.replace(/'/g, '"')));
}

function overflow() {
    var left = document.getElementById("left-overflow");
    var right = document.getElementById("right-overflow");
    var element = document.getElementById("myTab");
    if (element.offsetHeight < element.scrollHeight || element.offsetWidth < element.scrollWidth) {
        left.classList.remove("d-none");
        right.classList.remove("d-none");
    } else {
        left.classList.add("d-none");
        right.classList.add("d-none");
    }
}

function drawChart(data) {
    console.log(data);
    let chartStatus = Chart.getChart("chart");
    if (chartStatus != undefined) {
        chartStatus.destroy();
    }

    var labels = [];
    var values = [];

    for (var i = 0; i < data.data.thongKeTheoNgayTienThangNam.length; i++) {
        //var label = data.data[i].ngay + "/" + data.data[i].thang + "/" + data.data[i].nam;
        var label = "Ngày " + data.data.thongKeTheoNgayTienThangNam[i].ngay;
        var value = data.data.thongKeTheoNgayTienThangNam[i].tongTien;

        //console.log(label)
        //console.log(value)

        labels.push(label);
        values.push(value);
    }

    var ctx = document.getElementById("chart");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Tổng tiền',
                data: values,
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                borderColor: 'rgba(54, 162, 235, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            plugins: {
                legend: {
                    display: true,
                    position: 'top'
                },
                title: {
                    display: true,
                }
            }
        }
    });
}

window.previewImage = (inputElem, imgElem) => {
    const url = URL.createObjectURL(inputElem.files[0]);
    imgElem.addEventListener('load', () => URL.revokeObjectURL(url), { once: true });
    imgElem.src = url;
}

window.drawChart = drawChart;
window.storageImages = storageImages;
window.uploadImages = uploadImages;
window.choiceLoad = choiceLoad;
window.overflow = overflow;

