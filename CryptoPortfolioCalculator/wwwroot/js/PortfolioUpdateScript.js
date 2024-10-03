let intervalId = null

function formatCurrency(value) {
    if (value < 1) {
        return '$' + value;
    } else {
        return value.toLocaleString('en-US', { style: 'currency', currency: 'USD', minimumFractionDigits: 2 });
    }
}
jQuery(document).ready(function () {

    let refreshInterval = $('#refresh-interval').val() * 60000;  
    // Function that periodically fetches updates from the server and reflect new values to the current price,
    // the current value and percentage change
    function fetchPortfolioUpdates() {

        if (intervalId !== null) {
            clearInterval(intervalId);
            intervalId = null;
        }

        intervalId = setInterval(() => {
            $.ajax({
                url: '/Portfolio/GetUpdatedPortfolio',
                method: 'GET',
                success: function (data) {
                    data.forEach(function (coin) {

                        let row = $('tr[data-coin-name="' + coin.name + '"]');
                        row.find('.current-price').text(formatCurrency(coin.currentPrice));  
                        row.find('.current-value').text(formatCurrency(coin.currentValue));  
                        row.find('.percentage-change').text(coin.percentageChange.toFixed(2) + '%');
                        let currentdate = new Date();
                        console.log(currentdate)
                    });
                },
                error: function (error) {
                    console.error("Error fetching portfolio data", error);
                }
            });
        }, refreshInterval);
    }

    // When user changes the value of the minutes we update the refresh interval of the page
    // and trigger the fetchPortfolioUpdates function with new values
    $('#refresh-interval').on('change', function () {
        refreshInterval = $(this).val() * 60000;
        fetchPortfolioUpdates();
    });
    
    fetchPortfolioUpdates();
});