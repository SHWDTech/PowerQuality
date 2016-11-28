var voltageCurrentCharts = [];
var chartsOption = {};

chartsOption.StepLine = function (params) {
    var opt = {
        title: {
            text: params['titleName'],
            show: true,
            textStyle: {
                color: '#337ab7',
                fontSize: 14
            },
            textAlign: 'center',
            left: 'center'
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (formatParams) {
                var view = params['category'][formatParams[0]['dataIndex']].format("yyyy-MM-dd hh:mm:ss S");
                formatParams.forEach(function (par, index) {
                    if (index > 3) return;
                    view = view +
                        '<br/>' +
                        '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' +
                        par.color +
                        '"></span>' +
                        par['seriesName'] +
                        '：' +
                        par.data;
                });

                return view;
            },
            textStyle: {
                fontSize: 8
            },
            confine: true
        },
        legend: {
            show: false,
            data: ['最大值', '平均值', '最小值']
        },
        grid: {
            top: '0%',
            left: '0%',
            right: '0%',
            bottom: '0%',
            containLabel: true
        },
        toolbox: {
        },
        xAxis: {
            type: 'category',
            data: params['category'],
            axisLabel: {
                show: params['axisLabel'],
                formatter: function (value) {
                    return value.format("hh:mm:ss S");
                }
            }
        },
        yAxis: {
            type: 'value',
            max: params['maxVal'],
            min: params['minVal'],
            splitNumber: 6,
            minInterval: 0.001,
            interval: params['interval'],
            axisLabel: {
                show: true
            }
        },
        dataZoom: {
            type: 'inside',
            yAxisIndex: null,
            filterMode: 'filter',
            zoomLock: true
        },
        series: [
            {
                name: '最大值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['max']
            },
            {
                name: '平均值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['avg']
            },
            {
                name: '最小值',
                type: 'line',
                step: 'start',
                xAxisIndex: 0,
                yAxisIndex: 0,
                lineStyle: {
                    normal: {
                        width: 1
                    }
                },
                data: params['min']
            }
        ]
    };

    opt['toolbox'] = {
        show: true,
        feature: {
            dataZoom: {
                show: true,
                yAxisIndex: false
            },
            restore: {
                show: true
            }
        }
    };
    opt.grid.top = '25px';
    if (params['index'] !== 0) {
        opt.toolbox.top = -10000;
    }
    return opt;
};

chartsOption.StepLinePower = function (params) {
    var opt = {
        title: {
            text: params['titleName'],
            show: true,
            textStyle: {
                color: '#337ab7',
                fontSize: 14
            },
            textAlign: 'center',
            left: 'center'
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (formatParams) {
                var view = params['category'][formatParams[0]['dataIndex']].format("yyyy-MM-dd hh:mm:ss S");
                formatParams.forEach(function (par) {
                    view = view +
                        '<br/>' +
                        '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' +
                        par.color +
                        '"></span>' +
                        par['seriesName'] +
                        '：' +
                        par.data;
                });

                return view;
            },
            textStyle: {
                fontSize: 8
            },
            confine: true
        },
        legend: {
            show: false,
            data: params['legend']
        },
        grid: {
            top: '0%',
            left: '0%',
            right: '0%',
            bottom: '0%',
            containLabel: true
        },
        toolbox: {
        },
        xAxis: {
            type: 'category',
            data: params['category'],
            axisLabel: {
                show: params['axisLabel'],
                formatter: function (value) {
                    return value.format("hh:mm:ss S");
                }
            }
        },
        yAxis: [
            {
                type: 'value',
                splitNumber: 6,
                axisLabel: {
                    show: true
                }
            },
            {
                type: 'value',
                min: 0,
                max: 1,
                splitNumber: 6,
                axisLabel: {
                    show: true
                }
            }
        ],
        dataZoom: {
            type: 'inside',
            yAxisIndex: null,
            filterMode: 'filter',
            zoomLock: true
        },
        series: params['series']
    };

    opt['toolbox'] = {
        show: true,
        feature: {
            dataZoom: {
                show: true,
                yAxisIndex: false
            },
            restore: {
                show: true
            }
        }
    };
    opt.grid.top = '25px';
    if (params['index'] !== 0) {
        opt.toolbox.top = -10000;
    }
    return opt;
};

chartsOption.LineChart = function (params) {
    var opt = {
        title: {
            text: params['titleName'],
            show: true,
            textStyle: {
                color: '#337ab7',
                fontSize: 14
            },
            textAlign: 'center',
            left: 'center'
        },
        tooltip: {
            trigger: 'axis',
            formatter: function (formatParams) {
                var view = params['category'][formatParams[0]['dataIndex']].format("yyyy-MM-dd hh:mm:ss S");
                formatParams.forEach(function (par, index) {
                    if (index > 3) return;
                    view = view +
                        '<br/>' +
                        '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:9px;height:9px;background-color:' +
                        par.color +
                        '"></span>' +
                        par['seriesName'] +
                        '：' +
                        par.data +
                        '%';
                });

                return view;
            },
            textStyle: {
                fontSize: 8
            },
            confine: true
        },
        legend: {
            show: false,
            data: params['legend']
        },
        grid: {
            top: '0%',
            left: '0%',
            right: '0%',
            bottom: '0%',
            containLabel: true
        },
        toolbox: {
        },
        xAxis: {
            type: 'category',
            data: params['category'],
            axisLabel: {
                show: params['axisLabel'],
                formatter: function (value) {
                    return value.format("hh:mm:ss S");
                }
            }
        },
        yAxis: {
            type: 'value',
            splitNumber: 6,
            min: 0,
            minInterval: 0.001,
            axisLabel: {
                show: true
            }
        },
        dataZoom: {
            type: 'inside',
            yAxisIndex: null,
            filterMode: 'filter',
            zoomLock: true
        },
        series: params['series']
    };

    opt['toolbox'] = {
        show: true,
        feature: {
            dataZoom: {
                show: true,
                yAxisIndex: false
            },
            restore: {
                show: true
            }
        }
    };
    opt.grid.top = '25px';
    if (params['index'] !== 0) {
        opt.toolbox.top = -10000;
    }
    return opt;
};

chartsOption.LineSeries = function (params) {
    var serier = {
        name: params['name'],
        type: 'line',
        step: 'start',
        xAxisIndex: 0,
        yAxisIndex: 0,
        lineStyle: {
            normal: {
                width: 1
            }
        },
        data: params['data']
    }

    return serier;
}

chartsOption.StepSeries = function(params) {
    var serier = [
        {
            name: params['name'][0],
            type: 'line',
            step: 'start',
            xAxisIndex: 0,
            yAxisIndex: params['yAxisIndex'],
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            data: params['data']['max']
        },
        {
            name: params['name'][1],
            type: 'line',
            step: 'start',
            xAxisIndex: 0,
            yAxisIndex: params['yAxisIndex'],
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            data: params['data']['avg']
        },
        {
            name: params['name'][2],
            type: 'line',
            step: 'start',
            xAxisIndex: 0,
            yAxisIndex: params['yAxisIndex'],
            lineStyle: {
                normal: {
                    width: 1
                }
            },
            data: params['data']['min']
        }
    ];

    return serier;
}

chartsOption.pushData = function () {

};