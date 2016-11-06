var loadPercentage = 0;

var chartsConfig = {
    'scale': 0,
    'maxPoint': 0,
    'dataSource': null,
    start: 0,
    end: 14400
}

var powerDatas = {
    'activeValues': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.activeValues[propName])) {
                powerDatas.activeValues[propName] = [];
            }
            propValues.forEach(function (value) {
                powerDatas.activeValues[propName].push(value);
            });
        }
    },
    'harmonics': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.harmonics[propName])) {
                powerDatas.harmonics[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.harmonics[propName].push(value);
            });
        }
    },
    'voltageCurrentSecond': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.voltageCurrentSecond[propName])) {
                powerDatas.voltageCurrentSecond[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.voltageCurrentSecond[propName].push(value);
            });
        }
    },
    'voltageCurrentThreeSecond': {
        'pusuRange': function (propName, propValues) {
            if (IsNullOrEmpty(powerDatas.voltageCurrentThreeSecond[propName])) {
                powerDatas.voltageCurrentThreeSecond[propName] = [];
            }

            propValues.forEach(function (value) {
                powerDatas.voltageCurrentThreeSecond[propName].push(value);
            });
        }
    }
};

var powerAnalysis = {
    'selectChannel': [],
    'fetch': function (url, startIndex, totalRequest, target) {
        if (totalRequest <= 0) {
            loadPercentage += 25;
            powerAnalysis.loadComplete();
            return;
        }
        var requestCount = totalRequest > 3600 ? 3600 : totalRequest;
        base.AjaxGet(url, { StartIndex: startIndex, RequestCount: requestCount, RecordId: $('#recordId').attr('recordId') }, null, function (ret) {
            $.each(ret.recordData, function (key, value) {
                target.pusuRange(key, value);
            });
            powerAnalysis.fetch(url, startIndex + requestCount, totalRequest - 3600, target);
        });
    },
    'loadComplete': function () {
        $('#percentage').html(loadPercentage);
        if (loadPercentage === 100) {
            powerAnalysis.setupConfig();
            powerAnalysis.initCharts();
            $('#pageLoading').hide('slow');
            $('#mainContent').show('slow');
        }
    },
    'initCharts': function () {
        window.powerCharts.forEach(function(chart) {
            echarts.dispose(chart);
        });
        window.powerCharts = [];
        $('#chartContainer').empty();
        var maxHeight = 800;
        var chartHeights = maxHeight / powerAnalysis.selectChannel.length;
        powerAnalysis.selectChannel.forEach(function (channel, index) {
            var chart = document.createElement('div');
            $(chart).attr('id', channel + '_chart');
            $('#chartContainer').append(chart);
            $(chart).height(chartHeights);
            $(chart).width($('#chartHolder').width() - 50);
            var current = echarts.init(chart);
            if (index !== 0) {
                $(chart).css('margin-top', '-20px');
            }
            window.powerCharts.push(current);
            current.setOption(powerAnalysis.getChartOption(channel, index));
        });
        echarts.connect(window.powerCharts);

        window.powerCharts.forEach(function(chart) {
            chart.on('datazoom', function (params) {
                var start = params.batch[0].startValue;
                var end = params.batch[0].endValue;
                if (chartsConfig.scale === 3 && (end - start) * 3 < chartsConfig.maxPoint) {
                    chartsConfig.dataSource = powerDatas.voltageCurrentSecond;
                    chartsConfig.start = start * 3;
                    chartsConfig.end = end * 3;
                    chartsConfig.scale = 1;
                    powerAnalysis.initCharts();
                } else {
                    window.powerCharts.forEach(function (crt) {
                        var option = crt.getOption();
                        var arr = option.series[0].data.slice(start, end)
                            .concat(option.series[1].data.slice(start, end))
                            .concat(option.series[2].data.slice(start, end));
                        var max = Math.max.apply(null, arr);
                        var min = Math.min.apply(null, arr);
                        var interval = (max - min) / 4;
                        option.yAxis[0].max = Math.round((max + interval) * 100) / 100;
                        option.yAxis[0].min = Math.round((min - interval) * 100) / 100;
                        option.yAxis[0].interval = Math.round(interval * 100) / 100;
                        crt.setOption(option);
                    });
                }
            });
        });
    },
    'getChartOption': function (channel, index) {
        var max = chartsConfig.dataSource[channel + '_Max'].slice(chartsConfig.start, chartsConfig.end);
        var avg = chartsConfig.dataSource[channel + '_Avg'].slice(chartsConfig.start, chartsConfig.end);
        var min = chartsConfig.dataSource[channel + '_Min'].slice(chartsConfig.start, chartsConfig.end);
        var maxVal = Math.round(Math.max.apply(null, max.concat(avg).concat(min)) * 100) / 100;
        var minVal = Math.round(Math.min.apply(null, max.concat(avg).concat(min)) * 100) / 100;
        var interval = Math.round((maxVal - minVal) / 4 * 100) / 100;
        var par = {
            'category': chartsConfig.dataSource['RecordTime'].slice(chartsConfig.start, chartsConfig.end).map(function (date) {
                return new Date(date);
            }),
            'max': max,
            'avg': avg,
            'min': min,
            'maxVal': Math.round((maxVal + interval) * 100) / 100,
            'minVal': Math.round((minVal - interval) * 100) / 100,
            'interval': interval,
            'axisLabel': true,
            'index': index,
            'titleName': channel
        }

        if (index < powerAnalysis.selectChannel.length - 1) {
            par['axisLabel'] = false;
        }

        return chartsOption.StepLine(par);
    },
    'setChannels': function (selects) {
        powerAnalysis.selectChannel = [];
        $.each(selects, function (key, value) {
            if ($(value).is(':checked')) {
                powerAnalysis.selectChannel.push($(value).attr('id'));
            }
        });
    },
    'setupConfig': function () {
        powerAnalysis.setChannels($('#channelOptions input[type="checkbox"]'));
        chartsConfig.maxPoint = $('#chartHolder').width() - 100;
        if (powerDatas.voltageCurrentSecond['RecordTime'].length > chartsConfig.maxPoint) {
            chartsConfig.dataSource = powerDatas.voltageCurrentThreeSecond;
            chartsConfig.scale = 3;
        } else {
            chartsConfig.dataSource = powerDatas.voltageCurrentSecond;
            chartsConfig.scale = 1;
        }
    }
};

$(function () {
    $('#channelOptions input[type="checkbox"]').on('change',function(event) {
        if ($(event.target).attr('data-vol-type') === 'star') {
            $('[data-vol-type="triangle"]').prop('checked', false);
        } else if ($(event.target).attr('data-vol-type') === 'triangle') {
            $('[data-vol-type="star"]').prop('checked', false);
        }
        powerAnalysis.setChannels($('#channelOptions input[type="checkbox"]'));
        powerAnalysis.initCharts();
    });
    powerAnalysis.fetch('/PowerAnalysis/RecordData', 0, 14400, powerDatas.activeValues, powerAnalysis.loadComplete());
    powerAnalysis.fetch('/PowerAnalysis/RecordHarmonic', 0, 14400, powerDatas.harmonics, powerAnalysis.loadComplete());
    powerAnalysis.fetch('/PowerAnalysis/VoltageCurrentSecond', 0, 3600, powerDatas.voltageCurrentSecond, powerAnalysis.loadComplete());
    powerAnalysis.fetch('/PowerAnalysis/VoltageCurrentThreeSecond', 0, 1200, powerDatas.voltageCurrentThreeSecond, powerAnalysis.loadComplete());
});