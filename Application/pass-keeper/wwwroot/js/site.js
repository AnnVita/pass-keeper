// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your Javascript code.

function index(nodeList, el) {
    for (i = 0; i < nodeList.length; i++) {
        if (nodeList[i] == el) {
            return i;
        }
    }
    return -1;
}

function createTab() {
    var tabListElement = document.createElement('div');
    tabListElement.className = 'tab-control__item';
    tabListElement.innerHTML = 'FTP';
    return tabListElement;
}

function createTabContent() {
    var tabListElement = document.createElement('div');
    tabListElement.className = 'tab-control__inside';
    tabListElement.innerHTML = '<div class="tab-control__inside tab-control__inside_current"><div class="checker-tools"><div class="checker-tools__form"><div class="checker-tools__form-input-container"><div class="input-container"><div class="input-container__name">Host name:</div><input type="text" class="input-container__input"></div></div><div class="checker-tools__form-input-container checker-tools__form-input-container_small"><div class="input-container"><div class="input-container__name">Port:</div><input type="text" class="input-container__input"></div></div><div class="checker-tools__form-input-container"><div class="input-container"><div class="input-container__name">User name:</div><input type="text" class="input-container__input"></div></div><div class="checker-tools__form-input-container"><div class="input-container"><div class="input-container__name">Password:</div><input type="text" class="input-container__input"></div></div><div class="checker-tools__submit-container"><div class="checker-tools__submit-button">Проверить</div></div></div><div class="checker-tools__checking-results"><div class="checker-tools__icon"></div><div class="checker-tools__text">Result</div></div></div></div>';
    return tabListElement;
}

function tabsListActivateElem(tabsList, active, ignore) {
    active = (active == undefined) ? tabsList[0] : active;
    ignore = (ignore == undefined) ? tabsList[tabsList.length - 1] : ignore;
    var activeElement = -1;
    for (var i = 0; i < tabsList.length; i++) {
        if (tabsList[i] == active) {
            tabsList[i].classList.add('tab-control__item_current');
            activeElement = i;
        } else {
            tabsList[i].classList.remove('tab-control__item_current');
        }
    }
    return activeElement;
}

function switchTabContent(tabsContent, contentBlockNumber) {
    for (var i = 0; i < tabsContent.length; i++) {
        if (i == contentBlockNumber) {
            tabsContent[i].classList.add('tab-control__inside_current');
        } else {
            tabsContent[i].classList.remove('tab-control__inside_current');
        }
    }
}



function ready() {
    var tabsContainer = document.querySelector('#tab-control-list');
    var tabsList = document.querySelectorAll('#tab-control-list > .tab-control__item');

    var tabsContentContainer = document.querySelector('#tab-control-content');
    var tabsContent = document.querySelectorAll('#tab-control-content > .tab-control__inside');
    document.getElementById('tab-control-list').onclick= function (event) {
        var target = event.target;
        if (target.className == 'tab-control__item') {
            var contentBlockNumber = tabsListActivateElem(tabsList, target);
            switchTabContent(tabsContent, contentBlockNumber);
        } else if (target.className == 'tab-control__item tab-control__item_new') {
            var tabListElement = createTab();
            tabsContainer.insertBefore(tabListElement, tabsList[tabsList.length - 1]);
            tabsContent = tabsList = document.querySelectorAll('#tab-control-list > .tab-control__item');
            var contentBlockNumber = tabsListActivateElem(tabsList, tabsList[tabsList.length - 2]);
            var tabContentElement = createTabContent();
            tabsContentContainer.appendChild(tabContentElement);
            tabsContent = document.querySelectorAll('#tab-control-content > .tab-control__inside');
            switchTabContent(tabsContent, contentBlockNumber);
        }
    }
}

document.addEventListener('DOMContentLoaded', ready);
