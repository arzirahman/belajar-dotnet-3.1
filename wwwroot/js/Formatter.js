function formatMoney(number) {
    const amount = parseFloat(number);
    if (isNaN(amount)) {
        return "Invalid Number";
    }
    const options = { style: 'currency', currency: 'IDR', minimumFractionDigits: 0 };
    const formattedAmount = amount.toLocaleString('id-ID', options);
    return formattedAmount;
}

function formatDate(inputDate)
{
    const months = [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];
    const parts = inputDate.split(" ");
    const datePart = parts[0];
    const timePart = parts[1];
    const [day, monthIdx, year] = datePart.split("/");
    const month = months[parseInt(monthIdx) - 1];
    return `${day} ${month} ${year} ${timePart}`;
}