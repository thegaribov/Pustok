/**
 * Theme: Metrica - Responsive Bootstrap 5 Admin Dashboard
 * Author: Mannatthemes
 * Analytics Customers Js
 */


 
 var options = {
  chart: {
    height: 375,
    type: 'line',
   
    toolbar: {
      show: false
    }
  },
  stroke: {
    width: 3,
    curve: 'smooth'
  },
  series: [{
    name: 'Likes',
    data: [4, 3, 10, 9, 29, 19, 22, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5]
  }],
  xaxis: {
    type: 'datetime',
    categories: ['1/11/2000', '2/11/2000', '3/11/2000', '4/11/2000', '5/11/2000', '6/11/2000', '7/11/2000', '8/11/2000', '9/11/2000', '10/11/2000', '11/11/2000', '12/11/2000', '1/11/2001', '2/11/2001', '3/11/2001', '4/11/2001', '5/11/2001', '6/11/2001'],
    axisBorder: {
      show: true,
      color: '#bec7e0',
    },  
    axisTicks: {
      show: true,
      color: '#bec7e0',
    },    
  },
  colors:['#5766da'],
  markers: {
    size: 3,
    opacity: 0.9,
    colors: ["#fdb5c8"],
    strokeColors: '#fff',
    strokeWidth: 1,
    style: 'inverted', // full, hollow, inverted
    hover: {
      size: 5,
    }
  },
  yaxis: {
    min: -10,
    max: 40,
    title: {
      text: 'Engagement',
    },
  },
  grid: {
    row: {
      colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
      opacity: 0.2
    },
    strokeDashArray: 3.5,
  },
  responsive: [{
    breakpoint: 600,
    options: {
      chart: {
        toolbar: {
          show: false
        }
      },
      legend: {
        show: false
      },
    }
  }]
}

var chart = new ApexCharts(
  document.querySelector("#apex_line1"),
  options
);




var options5 = {
  series: [{
    name: 'New Visitors',
    data: [68, 44, 55, 57, 56, 61, 58, 63, 60, 66]
  }, {
      name: 'Unique Visitors',
      data: [51, 76, 85, 101, 98, 87, 105, 91, 114, 94]
  },],

  chart: {
  type: 'bar',
  width: 200,
  height: 35,
  sparkline: {
    enabled: true
  }
},
colors: ["#4d79f6", "#e0e7fd"],
plotOptions: {
  bar: {
    columnWidth: '50%'
  }
},
labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
xaxis: {
  crosshairs: {
    width: 2
  },
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart5 = new ApexCharts(document.querySelector("#bar-1"), options5);


var options7 = {
  series: [45],
  chart: {
  type: 'radialBar',
  width: 50,
  height: 50,
  sparkline: {
    enabled: true
  }
},
dataLabels: {
  enabled: false
},
plotOptions: {
  radialBar: {
    hollow: {
      margin: 0,
      size: '50%'
    },
    track: {
      margin: 0
    },
    dataLabels: {
      show: false
    }
  }
}
};

var chart7 = new ApexCharts(document.querySelector("#radialBar-1"), options7);


var options1 = {
  series: [{
  data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
}],
  chart: {
  type: 'line',
  width: 200,
  height: 35,
  sparkline: {
    enabled: true
  }
},
stroke: {
  show: true,
  curve: 'smooth',
  width: [2],
  lineCap: 'round',
},
tooltip: {
  fixed: {
    enabled: false
  },
  x: {
    show: false
  },
  y: {
    title: {
      formatter: function (seriesName) {
        return ''
      }
    }
  },
  marker: {
    show: false
  }
}
};

var chart1 = new ApexCharts(document.querySelector("#line-1"), options1);


//define data array
var tabledata = [
  {id:1, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-1.jpg'>", name:"Oli Bob", progress:12, gender:"male", rating:1, col:"red", dob:"19/02/1984", car:1},
  {id:2, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-2.jpg'>", name:"Mary May", progress:1, gender:"female", rating:2, col:"blue", dob:"14/05/1982", car:true},
  {id:3, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-3.jpg'>", name:"Christine Lobowski", progress:42, gender:"female", rating:0, col:"green", dob:"22/05/1982", car:"true"},
  {id:4, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-4.jpg'>", name:"Brendon Philips", progress:100, gender:"male", rating:1, col:"orange", dob:"01/08/1980"},
  {id:5, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-5.jpg'>", name:"Margret Marmajuke", progress:16, gender:"female", rating:5, col:"yellow", dob:"31/01/1999"},
  {id:6, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-6.jpg'>", name:"Frank Harbours", progress:38, gender:"male", rating:4, col:"red", dob:"12/05/1966", car:1},
  {id:1, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-7.jpg'>", name:"Oli Bob", progress:12, gender:"male", rating:1, col:"red", dob:"19/02/1984", car:1},
  {id:2, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-8.jpg'>", name:"Mary May", progress:1, gender:"female", rating:2, col:"blue", dob:"14/05/1982", car:true},
  {id:3, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-9.jpg'>", name:"Christine Lobowski", progress:42, gender:"female", rating:0, col:"green", dob:"22/05/1982", car:"true"},
  {id:4, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-10.jpg'>", name:"Brendon Philips", progress:100, gender:"male", rating:1, col:"orange", dob:"01/08/1980"},
  {id:5, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-2.jpg'>", name:"Margret Marmajuke", progress:16, gender:"female", rating:5, col:"yellow", dob:"31/01/1999"},
  {id:6, image: "<img alt='' style='width: 30px; height: 30px' class='rounded' src='assets/images/users/user-4.jpg'>", name:"Frank Harbours", progress:38, gender:"male", rating:4, col:"red", dob:"12/05/1966", car:1},
];


var table = new Tabulator("#datatable-1", {
  data:tabledata,           //load row data from array
  layout:"fitColumns",      //fit columns to width of table
  responsiveLayout:"collapse",  //hide columns that dont fit on the table
  tooltips:true,            //show tool tips on cells
  addRowPos:"top",          //when adding a new row, add it to the top of the table
  history:true,             //allow undo and redo actions on the table
  pagination:"local",       //paginate the data
  paginationSize:10,         //allow 7 rows per page of data
  movableColumns:true,      //allow column order to be changed
  resizableRows:true,       //allow row order to be changed
  initialSort:[             //set the initial sort order of the data
      {column:"name", dir:"asc"},
  ],
  columns:[  
      {title: "User", field: "image", formatter:"html", width:70},               //define the table columns
      {title:"Name", field:"name", editor:"input"},
      {title:"Task Progress", field:"progress", hozAlign:"left", formatter:"progress", editor:true},
      {title:"Gender", field:"gender", width:95, editor:"select", editorParams:{values:["male", "female"]}},
      {title:"Rating", field:"rating", formatter:"star", hozAlign:"center", width:100, editor:true},
      {title:"Color", field:"col", width:130, editor:"input"},
      {title:"Date Of Birth", field:"dob", width:130, sorter:"date", hozAlign:"center"},
      {title:"Driver", field:"car", width:90,  hozAlign:"center", formatter:"tickCross", sorter:"boolean", editor:true},
  ],
});


window.addEventListener('DOMContentLoaded', (event) => {
  chart.render();
  chart1.render();
  chart7.render();
  chart5.render();
});

