/**
 * Theme: Metrica - Responsive Bootstrap 5 Admin Dashboard
 * Author: Mannatthemes
 * CRM Dashboard Js
 */


   // Performance Report

  
   var options = {
    chart: {
        height: 320,
        type: 'area',
        width: '100%',
        stacked: true,
        toolbar: {
          show: false,
          autoSelected: 'zoom'
        },
    },
    colors: ['#2a77f4', '#a5c2f1'],
    dataLabels: {
        enabled: false
    },
    stroke: {
        curve: 'smooth',
        width: [1.5, 1.5],
        dashArray: [0, 4],
        lineCap: 'round',
    },
    grid: {
      padding: {
        left: 0,
        right: 0
      },
      strokeDashArray: 3,
    },
    markers: {
      size: 0,
      hover: {
        size: 0
      }
    },
    series: [{
        name: 'New Visits',
        data: [0,60,20,90,45,110,55,130,44,110,75,120]
    }, {
        name: 'Unique Visits',
        data: [0,45,10,75,35,94,40,115,30,105,65,110]
    }],
  
    xaxis: {
        type: 'month',
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        axisBorder: {
          show: true,
        },  
        axisTicks: {
          show: true,
        },                  
    },
    fill: {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        opacityFrom: 0.4,
        opacityTo: 0.3,
        stops: [0, 90, 100]
      }
    },
    
    tooltip: {
        x: {
            format: 'dd/MM/yy HH:mm'
        },
    },
    legend: {
      position: 'top',
      horizontalAlign: 'right'
    },
  }
  
  var chart = new ApexCharts(
    document.querySelector("#crm-dash"),
    options
  );
  
  chart.render();

  

  var options = {
    chart: {
        height: 205,
        type: 'donut',
    }, 
    plotOptions: {
      pie: {
        donut: {
          size: '85%'
        }
      }
    },
    dataLabels: {
      enabled: false,
      },
      stroke: {
        show: true,
        width: 2,
        colors: ['transparent']
    },
   
    series: [10, 65, 25,],
    legend: {
        show: false,
        position: 'bottom',
        horizontalAlign: 'center',
        verticalAlign: 'middle',
        floating: false,
        fontSize: '14px',
        offsetX: 0,
        offsetY: 5
    },
    labels: [ "Sent", "Opened", "Not Opened"],
    colors: ["#fdb5c8", "#2a76f4", "#67c8ff"],
   
    responsive: [{
        breakpoint: 600,
        options: {
          plotOptions: {
              donut: {
                customScale: 0.2
              }
            },        
            chart: {
                height: 200
            },
            legend: {
                show: false
            },
        }
    }],
  
    tooltip: {
      y: {
          formatter: function (val) {
              return   val + " %"
          }
      }
    }  
  }
  
  var chart = new ApexCharts(
    document.querySelector("#email_report"),
    options
  );
  
  chart.render();
//   var options = {
//     series: [{
//     name: 'Domestic',
//     data: [44, 55, 41, 67, 22, 43, 35]
//   }, {
//     name: 'International',
//     data: [13, 23, 20, 8, 13, 27, 45]
//   }, ],
//     chart: {
//     type: 'bar',
//     height: 265,
//     stacked: true,
//     toolbar: {
//       show: false
//     },
//     zoom: {
//       enabled: true
//     }
//   },

//   stroke: {
//     show: true,
//     width: 2,
//     colors: ['transparent']
// },
// colors: ['#2a76f4',"#fdb5c8", ],
//   responsive: [{
//     breakpoint: 480,
//     options: {
//       legend: {
//         position: 'bottom',
//         offsetX: -10,
//         offsetY: 0
//       }
//     }
//   }],
//   plotOptions: {
//       bar: {
//           horizontal: false,
//           columnWidth: '30%',
//       },
//   },
//   xaxis: {
//     type: 'categories',
//     categories: ['Sun','Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
  
//   },
//   grid: {
//     row: {
//         colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
//         opacity: 0.2,           
//     },
//     strokeDashArray: 2.5,
// },
//   legend: {
//     offsetY: 6,
//   },
//   fill: {
//     opacity: 1
//   }
//   };

//   var chart = new ApexCharts(document.querySelector("#widget1"), options);
//   chart.render();
