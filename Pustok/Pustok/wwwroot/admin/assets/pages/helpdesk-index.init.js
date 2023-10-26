/**
 * Theme: Dastyle - Responsive Bootstrap 4 Admin Dashboard
 * Author: Mannatthemes
 * Dashboard Js
 */

 var options = {
  chart: {
    height: 325,
    type: 'area',
    toolbar: {
      show: false
    },
  },
  colors: ['#67c8ff', '#6d81f5'],
  dataLabels: {
      enabled: false
  },
  stroke: {
      show: true,
      curve: 'smooth',
      width: [1.5, 1.5],
      dashArray: [0, 4],
      lineCap: 'round',
  },
  series: [{
      name: 'Income',
      data: [45,45,20,20,20,100,100,100,35,35,80,80]
  }, {
      name: 'Expenses',
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
   show: false
  },
  
  fill: {
      type:"gradient",
      gradient: {
          type: "vertical",
          shadeIntensity: 1,
          inverseColors: !1,
          opacityFrom: .28,
          opacityTo: .05,
          stops: [45, 100]
      }
  },
};

var chart = new ApexCharts(document.querySelector("#Tickets_Status"), options);
chart.render();

// saprkline chart


var dash_spark_1 = {
    
  chart: {
      type: 'area',
      height: 60,
      sparkline: {
          enabled: true
      },
  },
  stroke: {
      curve: 'smooth',
      width: 2
    },
  fill: {
      opacity: 1,
      gradient: {
        shade: '#2c77f4',
        type: "horizontal",
        shadeIntensity: 0.5,
        inverseColors: true,
        opacityFrom: 0.1,
        opacityTo: 0.1,
        stops: [0, 80, 100],
        colorStops: []
    },
  },
  series: [{
    data: [4, 8, 5, 10, 4, 16, 5, 11, 6, 11, 30, 10, 13, 4, 6, 3, 6]
  }],
  yaxis: {
      min: 0
  },
  colors: ['#2c77f4'],
  tooltip: {
    show: false,
  }
}
new ApexCharts(document.querySelector("#dash_spark_1"), dash_spark_1).render();


var dash_spark_2 = {
    
  chart: {
      type: 'area',
      height: 60,
      sparkline: {
          enabled: true
      },
  },
  stroke: {
      curve: 'smooth',
      width: 2
    },
  fill: {
      opacity: 1,
      gradient: {
        shade: '#fdb5c8',
        type: "horizontal",
        shadeIntensity: 0.5,
        inverseColors: true,
        opacityFrom: 0.1,
        opacityTo: 0.1,
        stops: [0, 80, 100],
        colorStops: []
    },
  },
  series: [{
    data: [4, 8, 5, 10, 4, 25, 5, 11, 6, 11, 5, 10, 3, 14, 6, 8, 6]
  }],
  yaxis: {
      min: 0
  },
  colors: ['#2c77f4'],
}
new ApexCharts(document.querySelector("#dash_spark_2"), dash_spark_2).render();



//Device-widget


var options = {
  chart: {
      height: 240,
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
  series: [65, 20, 10, 5],
  legend: {
      show: false,
      position: 'bottom',
      horizontalAlign: 'center',
      verticalAlign: 'middle',
      floating: false,
      fontSize: '14px',
      offsetX: 0,
      offsetY: -13
  },
  labels: [ "Excellent", "Very Good", "Good", "Fair"],
  colors: ["#2a76f4", "#fdb5c8", "#67c8ff", "#c693ff"],
 
  responsive: [{
      breakpoint: 600,
      options: {
        plotOptions: {
            donut: {
              customScale: 0.2
            }
          },        
          chart: {
              height: 240
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
  document.querySelector("#ana_device"),
  options
);

chart.render();

