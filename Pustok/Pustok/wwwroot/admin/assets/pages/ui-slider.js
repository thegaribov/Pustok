/**
 * Theme: Metrica - Responsive Bootstrap 4 Admin Dashboard
 * Author: Mannatthemes
 * Ui-slider Js
 */

//  default

var slider = document.getElementById('slider');
noUiSlider.create(slider, {
    start: [5],
    step: 1,
    range: {
        min: [0],
        max: [10]
    },   
});

slider.noUiSlider.on('hover', function( value ){
    console.log(value);
});

// // date slider


// var slider = document.getElementById('slider_2');
var hidingTooltipSlider = document.getElementById('slider_2');

noUiSlider.create(hidingTooltipSlider, {
    start: [20, 80],
    connect: true,
    range: {
        'min': 0,
        'max': 100
    },
    tooltips: true
});

// Range Slider
var range = document.getElementById('range');

noUiSlider.create(range, {

    range: {
        'min': 1300,
        'max': 3250
    },

    step: 150,

    // Handles start at ...
    start: [1450, 2050, 2350, 3000],

    // ... must be at least 300 apart
    margin: 300,

    // ... but no more than 600
    limit: 600,

    // Display colored bars between handles
    connect: true,

    // Put '0' at the bottom of the slider
    direction: 'rtl',
    orientation: 'vertical',

    // Move handle on tap, bars are draggable
    behaviour: 'tap-drag',
    tooltips: true,
    format: wNumb({
        decimals: 0
    }),

    // Show a scale with the slider
    pips: {
        mode: 'steps',
        stepped: true,
        density: 4
    }
});
// Give the slider dimensions
range.style.height = '400px';
range.style.margin = '0 auto 30px';

var valuesDivs = [
    document.getElementById('range-value-1'),
    document.getElementById('range-value-2'),
    document.getElementById('range-value-3'),
    document.getElementById('range-value-4')
];

var diffDivs = [
    document.getElementById('range-diff-1'),
    document.getElementById('range-diff-2'),
    document.getElementById('range-diff-3')
];

// When the slider value changes, update the input and span
range.noUiSlider.on('update', function (values, handle) {
    valuesDivs[handle].innerHTML = values[handle];
    diffDivs[0].innerHTML = values[1] - values[0];
    diffDivs[1].innerHTML = values[2] - values[1];
    diffDivs[2].innerHTML = values[3] - values[2];
});

// date

// Create a new date from a string, return as a timestamp.
function timestamp(str) {
    return new Date(str).getTime();
}
// Create a list of day and month names.
var weekdays = [
    "Sunday", "Monday", "Tuesday",
    "Wednesday", "Thursday", "Friday",
    "Saturday"
];

var months = [
    "January", "February", "March",
    "April", "May", "June", "July",
    "August", "September", "October",
    "November", "December"
];

// Append a suffix to dates.
// Example: 23 => 23rd, 1 => 1st.
function nth(d) {
    if (d > 3 && d < 21) return 'th';
    switch (d % 10) {
        case 1:
            return "st";
        case 2:
            return "nd";
        case 3:
            return "rd";
        default:
            return "th";
    }
}

// Create a string representation of the date.
function formatDate(date) {
    return weekdays[date.getDay()] + ", " +
        date.getDate() + nth(date.getDate()) + " " +
        months[date.getMonth()] + " " +
        date.getFullYear();
}
var dateSlider = document.getElementById('slider-date');

noUiSlider.create(dateSlider, {
// Create two timestamps to define a range.
    range: {
        min: timestamp('2010'),
        max: timestamp('2016')
    },

// Steps of one week
    step: 7 * 24 * 60 * 60 * 1000,

// Two more timestamps indicate the handle starting positions.
    start: [timestamp('2011'), timestamp('2015')],

// No decimals
    format: wNumb({
        decimals: 0
    })
});
var dateValues = [
    document.getElementById('event-start'),
    document.getElementById('event-end')
];
dateSlider.noUiSlider.on('update', function (values, handle) {
    dateValues[handle].innerHTML = formatDate(new Date(+values[handle]));
});

// HTML

var select = document.getElementById('input-select');

// Append the option elements
for (var i = -20; i <= 40; i++) {

    var option = document.createElement("option");
    option.text = i;
    option.value = i;

    select.appendChild(option);
}

var html5Slider = document.getElementById('html5');

noUiSlider.create(html5Slider, {
    start: [10, 30],
    connect: true,
    range: {
        'min': -20,
        'max': 40
    }
});

var inputNumber = document.getElementById('input-number');

html5Slider.noUiSlider.on('update', function (values, handle) {

    var value = values[handle];

    if (handle) {
        inputNumber.value = value;
    } else {
        select.value = Math.round(value);
    }
});

select.addEventListener('change', function () {
    html5Slider.noUiSlider.set([this.value, null]);
});

inputNumber.addEventListener('change', function () {
    html5Slider.noUiSlider.set([null, this.value]);
});

// Skipstep

var skipSlider = document.getElementById('skipstep');

noUiSlider.create(skipSlider, {
    range: {
        'min': 0,
        '10%': 10,
        '20%': 20,
        '30%': 30,
        // Nope, 40 is no fun.
        '50%': 50,
        '60%': 60,
        '70%': 70,
        // I never liked 80.
        '90%': 90,
        'max': 100
    },
    snap: true,
    start: [20, 90]
});

var skipValues = [
    document.getElementById('skip-value-lower'),
    document.getElementById('skip-value-upper')
];

skipSlider.noUiSlider.on('update', function (values, handle) {
    skipValues[handle].innerHTML = values[handle];
});

// Slider pips

var pipsSlider = document.getElementById('slider-pips');

noUiSlider.create(pipsSlider, {
    range: {
        min: 0,
        max: 100
    },
    start: [50],
    pips: {mode: 'count', values: 5}
});
var pips = pipsSlider.querySelectorAll('.noUi-value');

function clickOnPip() {
    var value = Number(this.getAttribute('data-value'));
    pipsSlider.noUiSlider.set(value);
}

for (var i = 0; i < pips.length; i++) {

    // For this example. Do this in CSS!
    pips[i].style.cursor = 'pointer';
    pips[i].addEventListener('click', clickOnPip);
}

// Soft

var softSlider = document.getElementById('soft');

noUiSlider.create(softSlider, {
    start: 50,
    range: {
        min: 0,
        max: 100
    },
    pips: {
        mode: 'values',
        values: [20, 80],
        density: 4
    },
    tooltips: true
});
softSlider.noUiSlider.on('change', function (values, handle) {
    if (values[handle] < 20) {
        softSlider.noUiSlider.set(20);
    } else if (values[handle] > 80) {
        softSlider.noUiSlider.set(80);
    }
});