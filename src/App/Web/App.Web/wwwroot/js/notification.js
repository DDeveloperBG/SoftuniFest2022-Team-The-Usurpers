window.addEventListener("load", () => {
    const notificationsInput = document.getElementById("notifications");
    const notificationsBellImg = document.getElementById("notify_bell");
    notificationsInput.addEventListener("change", notificationsStateChanged);
    setBellImg();

    function notificationsStateChanged() {
        setBellImg();
        fetch("/User/ChangeNotificationsState", { method: "POST" });
    }

    function setBellImg() {
        let bellState = notificationsInput.checked ? "on" : "off";
        notificationsBellImg.src = `/images/notification_${bellState}.png`;
    }
});
