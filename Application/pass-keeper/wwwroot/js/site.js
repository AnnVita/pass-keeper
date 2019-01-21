// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your Javascript code.

function ready() {
    var copyBtn = document.querySelector('.info-text__copy-button');  
    copyBtn.addEventListener('click', function(event) { 
        var range = document.getSelection().getRangeAt(0);
        range.selectNode(document.getElementById("item-to-copy"));
        window.getSelection().addRange(range);
        document.execCommand("copy")
    });
}

//document.addEventListener('DOMContentLoaded', ready);
window.onload = ready;