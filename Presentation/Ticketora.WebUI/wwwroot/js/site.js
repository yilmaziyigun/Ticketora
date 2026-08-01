// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const paymentPanel = document.querySelector(".booking-payment");

    if (!paymentPanel) {
        return;
    }

    const inputs = {
        name: paymentPanel.querySelector('[data-card-input="name"]'),
        number: paymentPanel.querySelector('[data-card-input="number"]'),
        expiry: paymentPanel.querySelector('[data-card-input="expiry"]'),
        cvv: paymentPanel.querySelector('[data-card-input="cvv"]')
    };

    const previews = {
        name: paymentPanel.querySelector('[data-card-preview="name"]'),
        number: paymentPanel.querySelector('[data-card-preview="number"]'),
        expiry: paymentPanel.querySelector('[data-card-preview="expiry"]'),
        cvv: paymentPanel.querySelector('[data-card-preview="cvv"]')
    };

    const formatCardNumber = (value) => value
        .replace(/\D/g, "")
        .slice(0, 16)
        .replace(/(.{4})/g, "$1 ")
        .trim();

    const maskCardNumber = (value) => {
        const digits = value.replace(/\D/g, "");

        if (!digits) {
            return "•••• •••• •••• 4820";
        }

        const lastFour = digits.slice(-4).padStart(4, "•");
        return `•••• •••• •••• ${lastFour}`;
    };

    const formatExpiry = (value) => {
        const digits = value.replace(/\D/g, "").slice(0, 4);

        if (digits.length <= 2) {
            return digits;
        }

        return `${digits.slice(0, 2)}/${digits.slice(2)}`;
    };

    const syncPreview = () => {
        const cardName = inputs.name?.value.trim();
        const cardNumber = inputs.number?.value ?? "";
        const expiry = inputs.expiry?.value.trim();
        const cvv = inputs.cvv?.value.trim();

        if (previews.name) {
            previews.name.textContent = cardName || "TICKETORA MİSAFİR";
        }

        if (previews.number) {
            previews.number.textContent = maskCardNumber(cardNumber);
        }

        if (previews.expiry) {
            previews.expiry.textContent = expiry || "12/29";
        }

        if (previews.cvv) {
            previews.cvv.textContent = cvv ? "•".repeat(Math.min(cvv.length, 3)) : "•••";
        }
    };

    inputs.number?.addEventListener("input", (event) => {
        event.target.value = formatCardNumber(event.target.value);
        syncPreview();
    });

    inputs.expiry?.addEventListener("input", (event) => {
        event.target.value = formatExpiry(event.target.value);
        syncPreview();
    });

    inputs.cvv?.addEventListener("input", (event) => {
        event.target.value = event.target.value.replace(/\D/g, "").slice(0, 3);
        syncPreview();
    });

    inputs.name?.addEventListener("input", syncPreview);
    syncPreview();
});
