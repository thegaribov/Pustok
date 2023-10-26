/**
 * Theme: Metrica - Responsive Bootstrap 5 Admin Dashboard
 * Author: Mannatthemes
 * Tree Js
 */

listree();

document.getElementsByClassName('listree')[0].querySelectorAll('.listree-submenu-heading').forEach(function (element,ind){
    if(ind==1 || ind==0){
        element.classList.remove('collapsed');
        element.classList.add('expanded');
    }
});


document.getElementsByClassName('listree')[1].querySelectorAll('.listree-submenu-heading').forEach(function (element,ind){
    if(ind==1 || ind==0){
        element.classList.remove('collapsed');
        element.classList.add('expanded');
    }
});



// document.getElementsByClassName('listree')[0].getElementsByClassName('listree-submenu-items').forEach(function (element,ind){
//     if(ind==1 || ind==0){
//         element.style.display = "block";
//     }
// });


// document.getElementsByClassName('listree')[1].getElementsByClassName('listree-submenu-items').forEach(function (element,ind){
//     if(ind==1 || ind==0){
//         element.style.display = "block";
//     }
// });