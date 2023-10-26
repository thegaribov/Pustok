/**
 * Theme: Metrica - Responsive Bootstrap 5 Admin Dashboard
 * Author: Mannatthemes
 * CRM Dashboard Js
 */


   // Performance Report

   var options = {
    chart: {
      height: 335,
      type: 'area',
      toolbar: {
        show: false
      },
      // dropShadow: {
      //   enabled: true,
      //   top: 12,
      //   left: 0,
      //   bottom: 0,
      //   right: 0,
      //   blur: 2,
      //   color: '#45404a2e',
      //   opacity: 0.35
      // },
    },
    colors: ['#67c8ff', '#6d81f5'],
    dataLabels: {
        enabled: false
    },
    stroke: {
        show: true,
        curve: 'smooth',
        width: [2, 2],
        dashArray: [0, 4],
        lineCap: 'round',
    },
    series: [{
        name: 'OPD',
        data: [10,10,50,20,70,20,80,30,75,40,60,60]
    }, {
        name: 'General Patients',
        data: [0, 30, 10, 40, 30, 60, 50, 80, 70, 100, 90, 130]
    }],
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    
    yaxis: {
      labels: {      
        offsetX: -12,
        offsetY: 0,      
      }
    },
    grid: {
      borderColor: '#e0e6ed',
      strokeDashArray: 3,
      xaxis: {
          lines: {
              show: true
          }
      },   
      yaxis: {
          lines: {
              show: false,
          }
      },
    }, 
    legend: {
      show: true,
      position: 'top',
      horizontalAlign: 'right',
  },
    
    fill: {
        type:"gradient",
        gradient: {
            type: "vertical",
            opacityFrom: .28,
            opacityTo: .05,
        }
    },
  };
  
  var chart = new ApexCharts(document.querySelector("#patients-survey"), options);
  chart.render();
  


  var options = {
    chart: {
      type: 'radialBar',
      height: 255,
    },
    plotOptions: {
      radialBar: {
        offsetY: -10,
        startAngle: 0,
        endAngle: 270,
        hollow: {
          margin: 5,
          size: '50%',
          background: 'transparent',  
        },
        track: {
          show: false,
        },
        dataLabels: {
          name: {
              fontSize: '18px',
          },
          value: {
              fontSize: '16px',
              color: '#50649c',
          },
          
        }
      },
    },
    colors: ["#2a76f4", "#67c8ff", "#fdb5c8"],
    stroke: {
      lineCap: 'round'
    },
    series: [71, 93, 10],
    labels: ['Active', 'Recovered', 'Deaths'],
    legend: {
      show: true,
      floating: true,
      position: 'left',
      offsetX: -10,
      offsetY: 0,
    },
    responsive: [{
        breakpoint: 480,
        options: {
            legend: {
                show: true,
                floating: true,
                position: 'left',
                offsetX: 10,
                offsetY: 0,
            }
        }
    }]
  }
  
  
  var chart = new ApexCharts(
    document.querySelector("#covid_status"),
    options
  );
  
  chart.render();


  

 //colunm-1
  
 var options = {
  chart: {
      height: 210,
      type: 'bar',
      toolbar: {
          show: false
      }
  },
  plotOptions: {
      bar: {
          horizontal: false,
          endingShape: 'rounded',
          columnWidth: '25%',
      },
  },
  dataLabels: {
      enabled: false
  },
  stroke: {
      show: true,
      width: 2,
      colors: ['transparent']
  },
  colors: ["#4d79f6", "#e0e7fd"],
  series: [{
      name: 'Male',
      data: [68, 44, 55, 57, 56, 61, 58]
  }, {
      name: 'Female',
      data: [51, 76, 85, 101, 98, 87, 105]
  },],
  xaxis: {
      categories: ['Sun','Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Set'],
      axisBorder: {
        show: true,
        color: '#bec7e0',
      },  
      axisTicks: {
        show: true,
        color: '#bec7e0',
      },    
  },
  legend: {
    show: false,
    position: 'top',
    horizontalAlign: 'right',
  },
 
  fill: {
      opacity: 1

  },
  // legend: {
  //     floating: true
  // },
  grid: {
      row: {
          colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
          opacity: 0.2
      },
      borderColor: '#f1f3fa',
      strokeDashArray: 3,
  },
  tooltip: {
      y: {
          formatter: function (val) {
              return "" + val 
          }
      }
  }
}

var chart = new ApexCharts(
  document.querySelector("#patient_dash_report"),
  options
);

chart.render();