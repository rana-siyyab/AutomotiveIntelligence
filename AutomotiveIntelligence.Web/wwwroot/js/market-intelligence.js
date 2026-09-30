document.addEventListener("DOMContentLoaded", function () {

    if (typeof Chart === "undefined") {

        console.error(
            "Chart.js is not loaded."
        );

        return;
    }


    /*
     * =========================================================
     * HELPERS
     * =========================================================
     */

    function getPoints(selector) {

        return Array.from(
            document.querySelectorAll(selector)
        );
    }


    function getAttributeValues(
        selector,
        nameAttribute,
        countAttribute
    ) {

        const points = getPoints(selector);

        const labels = [];
        const values = [];

        points.forEach(point => {

            const name =
                point.getAttribute(nameAttribute);

            const count =
                Number(
                    point.getAttribute(countAttribute)
                );

            if (!name ||
                Number.isNaN(count)) {

                return;
            }

            labels.push(name);
            values.push(count);
        });

        return {
            labels,
            values
        };
    }


    function formatMillions(value) {

        if (value >= 1000000) {

            return (
                value / 1000000
            ).toFixed(1) + "M";
        }

        if (value >= 1000) {

            return (
                value / 1000
            ).toFixed(0) + "K";
        }

        return value.toLocaleString();
    }


    function createHorizontalBarChart(
        canvasId,
        labels,
        values
    ) {

        const canvas =
            document.getElementById(canvasId);

        if (!canvas ||
            labels.length === 0) {

            return;
        }


        new Chart(canvas, {

            type: "bar",

            data: {

                labels: labels,

                datasets: [

                    {
                        data: values,

                        borderWidth: 0,

                        borderRadius: 5,

                        backgroundColor:
                            "#00A6A6"
                    }

                ]

            },


            options: {

                indexAxis: "y",

                responsive: true,

                maintainAspectRatio: false,

                plugins: {

                    legend: {
                        display: false
                    },

                    tooltip: {

                        callbacks: {

                            label: function (context) {

                                return (
                                    " " +
                                    Number(
                                        context.raw
                                    ).toLocaleString() +
                                    " records"
                                );
                            }

                        }

                    }

                },


                scales: {

                    x: {

                        beginAtZero: true,

                        grid: {
                            color: "#E2E8F0"
                        },

                        ticks: {

                            color: "#718096",

                            font: {
                                size: 9
                            },

                            callback: function (value) {

                                return formatMillions(
                                    Number(value)
                                );
                            }

                        }

                    },


                    y: {

                        grid: {
                            display: false
                        },

                        ticks: {

                            color: "#475569",

                            font: {
                                size: 9
                            }

                        }

                    }

                }

            }

        });
    }


    /*
     * =========================================================
     * PRICE TREND
     * =========================================================
     */

    const trendCanvas =
        document.getElementById(
            "marketPriceChart"
        );

    const trendPoints =
        getPoints(
            ".market-trend-point"
        );


    if (trendCanvas &&
        trendPoints.length > 0) {

        const labels = [];
        const averagePrices = [];
        const minimumPrices = [];
        const maximumPrices = [];


        trendPoints.forEach(point => {

            const date =
                point.getAttribute(
                    "data-date"
                );

            const average =
                Number(
                    point.getAttribute(
                        "data-price"
                    )
                );

            const minimum =
                Number(
                    point.getAttribute(
                        "data-min"
                    )
                );

            const maximum =
                Number(
                    point.getAttribute(
                        "data-max"
                    )
                );


            if (!date ||
                Number.isNaN(average)) {

                return;
            }


            const formattedDate =
                new Date(
                    date + "T00:00:00"
                ).toLocaleDateString(
                    "en-GB",
                    {
                        day: "2-digit",
                        month: "short"
                    }
                );


            labels.push(
                formattedDate
            );

            averagePrices.push(
                average
            );

            minimumPrices.push(
                Number.isNaN(minimum)
                    ? null
                    : minimum
            );

            maximumPrices.push(
                Number.isNaN(maximum)
                    ? null
                    : maximum
            );

        });


        if (averagePrices.length > 0) {

            new Chart(
                trendCanvas,
                {

                    type: "line",

                    data: {

                        labels: labels,

                        datasets: [

                            {
                                label:
                                    "Average Price",

                                data:
                                    averagePrices,

                                tension:
                                    0.35,

                                borderWidth:
                                    2,

                                pointRadius:
                                    3,

                                pointHoverRadius:
                                    6,

                                fill:
                                    true,

                                backgroundColor:
                                    "rgba(0, 166, 166, 0.10)",

                                borderColor:
                                    "#00A6A6",

                                pointBackgroundColor:
                                    "#00A6A6",

                                pointBorderColor:
                                    "#FFFFFF",

                                pointBorderWidth:
                                    2
                            },


                            {
                                label:
                                    "Minimum",

                                data:
                                    minimumPrices,

                                tension:
                                    0.35,

                                borderWidth:
                                    1,

                                pointRadius:
                                    2,

                                borderColor:
                                    "#94A3B8",

                                pointBackgroundColor:
                                    "#94A3B8"
                            },


                            {
                                label:
                                    "Maximum",

                                data:
                                    maximumPrices,

                                tension:
                                    0.35,

                                borderWidth:
                                    1,

                                pointRadius:
                                    2,

                                borderColor:
                                    "#CBD5E1",

                                pointBackgroundColor:
                                    "#CBD5E1"
                            }

                        ]

                    },


                    options: {

                        responsive: true,

                        maintainAspectRatio:
                            false,

                        interaction: {

                            intersect:
                                false,

                            mode:
                                "index"
                        },


                        plugins: {

                            legend: {

                                display:
                                    true,

                                position:
                                    "bottom",

                                labels: {

                                    boxWidth:
                                        10,

                                    boxHeight:
                                        10,

                                    padding:
                                        15,

                                    font: {
                                        size: 10
                                    }

                                }

                            },


                            tooltip: {

                                callbacks: {

                                    label:
                                        function (
                                            context
                                        ) {

                                            const value =
                                                Number(
                                                    context.raw
                                                );

                                            if (
                                                Number.isNaN(
                                                    value
                                                )
                                            ) {

                                                return "";
                                            }


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

                                    color:
                                        "#718096",

                                    font: {
                                        size: 10
                                    }

                                }

                            },


                            y: {

                                beginAtZero:
                                    false,

                                grid: {

                                    color:
                                        "#E2E8F0"
                                },

                                ticks: {

                                    color:
                                        "#718096",

                                    font: {
                                        size: 10
                                    },

                                    callback:
                                        function (
                                            value
                                        ) {

                                            return (
                                                Number(
                                                    value
                                                ) /
                                                1000000
                                            ).toFixed(1) +
                                                "M";
                                        }

                                }

                            }

                        }

                    }

                }
            );
        }
    }


    /*
     * =========================================================
     * MARKET BY MAKE
     * =========================================================
     */

    const makeData =
        getAttributeValues(
            ".market-make-point",
            "data-name",
            "data-count"
        );


    createHorizontalBarChart(
        "marketMakeChart",
        makeData.labels,
        makeData.values
    );


    /*
     * =========================================================
     * TOP MODELS
     * =========================================================
     */

    const modelData =
        getAttributeValues(
            ".market-model-point",
            "data-name",
            "data-count"
        );


    createHorizontalBarChart(
        "marketModelChart",
        modelData.labels,
        modelData.values
    );


    /*
     * =========================================================
     * MARKET BY CITY
     * =========================================================
     */

    const cityData =
        getAttributeValues(
            ".market-city-point",
            "data-name",
            "data-count"
        );


    createHorizontalBarChart(
        "marketCityChart",
        cityData.labels,
        cityData.values
    );

});