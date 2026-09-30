$(document).ready(function () {

    const dataElement =
        document.getElementById('adminDashboardData');

    if (!dataElement) {
        return;
    }

    const dashboardData =
        JSON.parse(dataElement.textContent);

    renderPriceTrend(
        dashboardData.priceTrends || []
    );

    renderPopularModels(
        dashboardData.popularModels || []
    );

    renderPamaChart(
        dashboardData.pamaYearlySummary || []
    );

    renderOicaChart(
        dashboardData.oicaYearlySummary || []
    );

});


function renderPriceTrend(data) {

    if (!data.length) {
        showNoData('priceTrendChart');
        return;
    }

    const categories = data.map(x =>
        formatDate(x.date)
    );

    const values = data.map(x =>
        x.averagePrice
    );


    Highcharts.chart('priceTrendChart', {

        chart: {
            type: 'line'
        },

        title: {
            text: null
        },

        xAxis: {
            categories: categories,
            title: {
                text: 'Date'
            }
        },

        yAxis: {

            title: {
                text: 'Average Price (PKR)'
            },

            labels: {
                formatter: function () {
                    return formatMillions(this.value);
                }
            }
        },

        tooltip: {

            valueDecimals: 0,

            valuePrefix: '₨ '
        },

        series: [
            {
                name: 'Average Market Price',
                data: values
            }
        ],

        exporting: {
            enabled: true,
            showTable: false
        },

        credits: {
            enabled: false
        },

        accessibility: {
            description:
                'Average used-car market price over the selected period.'
        }

    });
}


function renderPopularModels(data) {

    if (!data.length) {
        showNoData('popularModelsChart');
        return;
    }

    const categories = data.map(x =>
        x.modelName
    );

    const values = data.map(x =>
        x.recordCount
    );

    Highcharts.chart('popularModelsChart', {

        chart: {
            type: 'bar'
        },

        title: {
            text: null
        },

        xAxis: {
            categories: categories,

            title: {
                text: null
            }
        },

        yAxis: {

            min: 0,

            title: {
                text: 'Market Records'
            }
        },

        tooltip: {
            valueSuffix: ' records'
        },

        series: [
            {
                name: 'Market Records',
                data: values
            }
        ],

        exporting: {
            enabled: true,
            showTable: false
        },

        credits: {
            enabled: false
        },

        accessibility: {
            description:
                'Most frequently observed vehicle models in the market data.'
        }

    });
}


function renderPamaChart(data) {

    if (!data.length) {
        showNoData('pamaChart');
        return;
    }

    const categories = data.map(x =>
        x.year.toString()
    );

    const production = data.map(x =>
        x.productionUnits
    );

    const sales = data.map(x =>
        x.salesUnits
    );


    Highcharts.chart('pamaChart', {

        chart: {
            type: 'column'
        },

        title: {
            text: null
        },

        xAxis: {

            categories: categories,

            title: {
                text: 'Year'
            }
        },

        yAxis: {

            title: {
                text: 'Units'
            }
        },

        tooltip: {
            shared: true
        },

        series: [

            {
                name: 'Production',
                data: production
            },

            {
                name: 'Sales',
                data: sales
            }

        ],

        exporting: {
            enabled: true,
            showTable: false
        },

        credits: {
            enabled: false
        },

        accessibility: {
            description:
                'PAMA yearly vehicle production and sales.'
        }

    });
}


function renderOicaChart(data) {

    if (!data.length) {
        showNoData('oicaChart');
        return;
    }

    const categories = data.map(x =>
        x.year.toString()
    );

    const production = data.map(x =>
        x.productionUnits
    );


    Highcharts.chart('oicaChart', {

        chart: {
            type: 'line'
        },

        title: {
            text: null
        },

        xAxis: {

            categories: categories,

            title: {
                text: 'Year'
            }
        },

        yAxis: {

            title: {
                text: 'Production Units'
            },

            labels: {
                formatter: function () {
                    return formatMillions(this.value);
                }
            }
        },

        tooltip: {
            valueSuffix: ' units'
        },

        series: [

            {
                name: 'Global Production',
                data: production
            }

        ],

        exporting: {
            enabled: true,
            showTable: false
        },

        credits: {
            enabled: false
        },

        accessibility: {
            description:
                'OICA global vehicle production by year.'
        }

    });
}


function formatDate(value) {

    if (!value) {
        return '';
    }

    const date = new Date(value);

    return date.toLocaleDateString(
        'en-GB',
        {
            day: '2-digit',
            month: 'short'
        }
    );
}


function formatMillions(value) {

    if (value >= 1000000) {
        return `${(value / 1000000).toFixed(1)}M`;
    }

    if (value >= 1000) {
        return `${(value / 1000).toFixed(0)}K`;
    }

    return value;
}


function showNoData(elementId) {

    const element =
        document.getElementById(elementId);

    if (!element) {
        return;
    }

    element.innerHTML =
        '<div class="chart-no-data">' +
        'No data available.' +
        '</div>';
}