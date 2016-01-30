'use strict';

myselflogApp.factory('valuesService', function () {
    function getTerapiesByYear (terapies, date) {
        var fastvalues = [];
        var slowvalues = [];
        var d2 = new Date(date);
        angular.forEach(terapies, function (item) {
            var d = new Date(item.logdate);
            if (d.getYear() == d2.getYear()) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }
    
    function getLogsByDay (profilelogs, date) {
        var logs = [];
        angular.forEach(profilelogs, function (item) {
            var a1 = moment(item.logdate).startOf('day').toDate();
            var a2 = moment(date).startOf('day').toDate();
            if (+a1 === +a2) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getLogsByWeeks(profilelogs, date) {
        var logs = [];
        var weekdates = getWeekDays(date);
        angular.forEach(profilelogs, function (item) {
            angular.forEach(weekdates, function (weekDay) {
                var a1 = moment(item.logdate).startOf('day').toDate();
                var a2 = weekDay.startOf('day').toDate();
                if (+a1 === +a2) {
                    logs.push(item);
                }
            });
        });
        return logs;
    }
    
    function getWeekDays(date) {
        var sunday = moment(date).day("Sunday");
        var monday = moment(date).day("Monday");
        var tuesday = moment(date).day("Tuesday");
        var wednesday = moment(date).day("Wednesday‎");
        var thursday = moment(date).day("Thursday");
        var friday = moment(date).day("Friday");
        var saturday = moment(date).day("Saturday");
        var weekdates = [sunday, monday, tuesday, wednesday, thursday, friday, saturday];
        return weekdates;
    }
    
    function getLogsByMonth(profilelogs, date) {
        var logs = [];
        var d2 = new Date(date);
        angular.forEach(profilelogs, function (item) {
            var d = new Date(item.logdate);
            if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getLogsByYear(profilelogs, date) {
        var logs = [];
        var d2 = new Date(date);
        angular.forEach(profilelogs, function (item) {
            var d = new Date(item.logdate);
            if (d.getYear() == d2.getYear()) {
                logs.push(item);
            }
        });
        return logs;
    }
    
    function getTerapiesByMonth(terapies, date) {
        var fastvalues = [];
        var slowvalues = [];
        var d2 = new Date(date);
        angular.forEach(terapies, function (item) {
            var d = new Date(item.logdate);
            if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }
    
    function getTerapiesByWeeks(terapies, date) {
        var fastvalues = [];
        var slowvalues = [];
        var weekdates = getWeekDays(date);
        angular.forEach(terapies, function (item) {
            angular.forEach(weekdates, function (weekDay) {
                var a1 = moment(item.logdate).startOf('day').toDate();
                var a2 = weekDay.startOf('day').toDate();
                if (+a1 === +a2) {
                    if (item.isslow) {
                        slowvalues.push(item);
                    } else {
                        fastvalues.push(item);
                    }
                }
            });
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }
    
    function getTerapiesByDay(terapies, date) {
        var fastvalues = [];
        var slowvalues = [];
        angular.forEach(terapies, function (item) {
            var a1 = moment(item.logdate).startOf('day').toDate();
            var a2 = moment(date).startOf('day').toDate();
            if (+a1 === +a2) {
                if (item.isslow) {
                    slowvalues.push(item);
                } else {
                    fastvalues.push(item);
                }
            }
        });
        return { 'fastvalues': fastvalues, 'slowvalues': slowvalues };
    }

    function getFoodsByDay(foods, date) {
        var items = [];
        angular.forEach(foods, function (item) {
            var a1 = moment(item.logdate).startOf('day').toDate();
            var a2 = moment(date).startOf('day').toDate();
            if (+a1 === +a2) {
                items.push(item);
            }
        });
        return items;
    }

    function getFoodsByWeeks(foods, date) {
        var items = [];
        var weekdates = getWeekDays(date);
        angular.forEach(foods, function (item) {
            angular.forEach(weekdates, function (weekDay) {
                var a1 = moment(item.logdate).startOf('day').toDate();
                var a2 = weekDay.startOf('day').toDate();
                if (+a1 === +a2) {
                    items.push(item);
                }
            });
        });
        return items;
    }

    function getFoodsByMonth(foods, date) {
        var items = [];
        var d2 = new Date(date);
        angular.forEach(foods, function (item) {
            var d = new Date(item.logdate);
            if ((d.getYear() == d2.getYear()) && (d.getMonth() == d2.getMonth())) {
                items.push(item);
            }
        });
        return items;
    }

    function getFoodsByYear(foods, date) {
        var items = [];
        var d2 = new Date(date);
        angular.forEach(foods, function (item) {
            var d = new Date(item.logdate);
            if (d.getYear() == d2.getYear()) {
                items.push(item);
            }
        });
        return items;
    }

    return {
        getLogs: function(reportname, logs, date) {
            if (reportname == "Year") {
                return getLogsByYear(logs, date);
            }
            if (reportname == "Month") {
                return getLogsByMonth(logs, date);
            }
            if (reportname == "Week") {
                return getLogsByWeeks(logs, date);
            }
            if (reportname == "Day") {
                return getLogsByDay(logs, date);
            }
            return null;
        },
        getTerapies: function(reportname, logterapies, date) {
            var terapies = { 'slow': [], 'fast': [] };
            var terapyObject = {};
            if (reportname == "Year") {
                terapyObject = getTerapiesByYear(logterapies, date);
            }
            if (reportname == "Month") {
                terapyObject = getTerapiesByMonth(logterapies, date);
            }
            if (reportname == "Week") {
                terapyObject = getTerapiesByWeeks(logterapies, date);
            }
            if (reportname == "Day") {
                terapyObject = getTerapiesByDay(logterapies, date);
            }
            // Fill terapies graph data starting with fast values
            angular.forEach(terapyObject.fastvalues, function(item) {
                terapies.fast.push({ 'logdate': item.logdate, 'fast': item.terapyvalue });
            });
            // Fill slow values
            angular.forEach(terapyObject.slowvalues, function(item) {
                terapies.slow.push({ 'logdate': item.logdate, 'slow': item.terapyvalue });
            });

            return terapies;
        },
        getCalories: function (reportname, foods, date) {
            var foodObject = {};
            if (reportname === "Year") {
                foodObject = getFoodsByYear(foods, date);
            }
            if (reportname === "Month") {
                foodObject = getFoodsByMonth(foods, date);
            }
            if (reportname === "Week") {
                foodObject = getFoodsByWeeks(foods, date);
            }
            if (reportname === "Day") {
                foodObject = getFoodsByDay(foods, date);
            }
            return foodObject;
        }
    };
})