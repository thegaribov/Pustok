/**
 * Theme: Metrica - Responsive Bootstrap 4 Admin Dashboard
 * Author: Mannatthemes
 * Wallet Js
 */



var options = {
  series: [76],
  chart: {
  type: 'radialBar',
  offsetY: 0,
  sparkline: {
    enabled: true
  }
},
plotOptions: {
  radialBar: {
    startAngle: -90,
    endAngle: 90,  
    hollow: {
      size: '75%',
      position: 'front',
  },  
    track: {
      background: ["rgba(42, 118, 244, .18)"],
      strokeWidth: '80%',
      opacity: 0.5,
      margin: 5,
    },
    dataLabels: {
      name: {
        show: false
      },
      value: {
        offsetY: -2,
        fontSize: '20px'
      }
    }
  }
},
stroke: {
  lineCap: 'butt'
},
colors: ["#2a76f4"],
grid: {
  padding: {
    top: -10
  }
},

labels: ['Average Results'],
};

var chart = new ApexCharts(document.querySelector("#wallet_ratio"), options);
chart.render();