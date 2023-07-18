function formatMoney(number) {
    const amount = parseFloat(number);
    if (isNaN(amount)) {
        return "Invalid Number";
    }
    const options = { style: 'currency', currency: 'IDR', minimumFractionDigits: 0 };
    const formattedAmount = amount.toLocaleString('id-ID', options);
    return formattedAmount;
}