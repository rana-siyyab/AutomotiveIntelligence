document.addEventListener("DOMContentLoaded", function() {

    const chartCanvas =
        document.getElementById("priceTrendChart");

    const trendData =
        document.querySelectorAll(".price-trend-point");

    if (!chartCanvas || trendData.length === 0) {
        return;
    }

    if (typeof Chart === "undefined") {
        console.error("Chart.js is not loaded.");
        return;
    }

    const labels = [];
    const prices = [];

    trendData.forEach(point => {

        const date =
            point.getAttribute("data-date");

        const price =
            Number(point.getAttribute("data-price"));

        if (!date || Number.isNaN(price)) {
            return;
        }

        const formattedDate =
            new Date(date + "T00:00:00")
                .toLocaleDateString(
                    "en-GB",
                    {
                        day: "2-digit",
                        month: "short"
                    }
                );

        labels.push(formattedDate);
        prices.push(price);
    });

    if (prices.length === 0) {
        return;
    }

    new Chart(chartCanvas, {

        type: "line",

        data: {
            labels: labels,

            datasets: [
                {
                    label: "Average Market Price",

                    data: prices,

                    tension: 0.35,

                    borderWidth: 2,

                    pointRadius: 4,

                    pointHoverRadius: 6,

                    fill: true,

                    backgroundColor:
                        "rgba(0, 166, 166, 0.10)",

                    borderColor:
                        "#00A6A6",

                    pointBackgroundColor:
                        "#00A6A6",

                    pointBorderColor:
                        "#FFFFFF",

                    pointBorderWidth: 2
                }
            ]
        },

        options: {

            responsive: true,

            maintainAspectRatio: false,

            interaction: {
                intersect: false,
                mode: "index"
            },

            plugins: {

                legend: {
                    display: false
                },

                tooltip: {

                    callbacks: {

                        label: function(context) {

                            const value =
                                Number(context.raw);

                            return (
                                " " +
                                value.toLocaleString() +
                                " PKR"
                            );
                        }
                    }
                }
            },

            scales: {

                x: {

                    grid: {
                        display: false
                    },

                    ticks: {
                        color: "#718096",

                        font: {
                            size: 11
                        }
                    }
                },

                y: {

                    beginAtZero: false,

                    grid: {
                        color: "#E2E8F0"
                    },

                    ticks: {

                        color: "#718096",

                        font: {
                            size: 11
                        },

                        callback: function(value) {

                            return (
                                (value / 1000000)
                                    .toFixed(1) +
                                "M"
                            );
                        }
                    }
                }
            }
        }
    });

});