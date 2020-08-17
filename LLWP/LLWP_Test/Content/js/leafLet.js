var main_Map = L.map('mainMap');
        var LLWPIcon = new L.Icon({
            iconUrl: '../Content/images/LLWP_ICON2.png',
            iconSize: [60, 80],
            iconAnchor: [27, 80],
            popupAnchor: [1, -34],
            shadowSize: [41, 41]
        });
        main_Map.setView(new L.LatLng(24.746583, 121.763135), 16);
        var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
        var osm = new L.TileLayer(osmUrl, { minZoom: 8, maxZoom: 16 });
        main_Map.addLayer(osm);

        L.marker([24.746583, 121.763135], { icon: LLWPIcon }).addTo(main_Map)

        main_Map.scrollWheelZoom.disable();
        main_Map.on('focus', () => { main_Map.scrollWheelZoom.enable(); });
        main_Map.on('blur', () => { main_Map.scrollWheelZoom.disable(); });

        // 建立 Leaflet 地圖
        var subMap = L.map('subMap');
        var LLWPIcon = new L.Icon({
            iconUrl: '../Content/images/LLWP_ICON2.png',
            iconSize: [60, 80],
            iconAnchor: [27, 80],
            popupAnchor: [1, -34],
            shadowSize: [41, 41]
        });

        // 建立圖標
        var greenIcon = new L.Icon({
            iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41]
        });

        // 設定經緯度座標
        subMap.setView(new L.LatLng(24.746583, 121.763135), 16);

        // 設定圖資來源
        var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
        var osm = new L.TileLayer(osmUrl, {
            minZoom: 8, maxZoom: 16,
        });

        // 將圖資來源加入至map內
        subMap.addLayer(osm);

        // 設定主icon位置
        L.marker([24.746583, 121.763135], { icon: LLWPIcon }).addTo(subMap)

        // 預計搬到json內
        var Data = [
            { 'type': 1, 'name': '炉匠炉端燒', 'web': 'https://www.lujiangangeline.com/', lat: 24.7469675, lng: 121.7613505 },
            { 'type': 1, 'name': '春滿園 好好味', 'web': 'https://www.facebook.com/HoMeiDimSum', lat: 24.7486941, lng: 121.7571906 },
            { 'type': 1, 'name': '廣食堂美食海鮮', 'web': 'https://www.facebook.com/food039386818/', lat: 24.7489743, lng: 121.771423 },
            { 'type': 2, 'name': '家樂福', 'web': 'https://www.carrefour.com.tw/', lat: 24.7431406, lng: 121.7568737 },
            { 'type': 2, 'name': 'MITSUI OUTLET PARK', 'web': 'https://www.mop.com.tw/linkou/tw/index.html', lat: 24.7478717, lng: 121.7579848 },
            { 'type': 2, 'name': 'COSTCO', 'web': 'https://www.costco.com.tw/', lat: 24.7501167, lng: 121.7661405 },
            { 'type': 3, 'name': '7-ELEVEN 校舍門市', 'web': 'https://www.7-11.com.tw/', lat: 24.7472172, lng: 121.7623247 },
            { 'type': 3, 'name': '萊爾富便利商店 宜蘭校舍店', 'web': 'https://www.hilife.com.tw/', lat: 24.7489551, lng: 121.7657668 },
            { 'type': 3, 'name': '全家便利商店 宜蘭凱旋店', 'web': 'https://www.family.com.tw/Marketing/index.aspx', lat: 24.7445802, lng: 121.7587349 },
            { 'type': 4, 'name': '綺麗寵物世界', 'web': 'https://www.facebook.com/pages/category/Pet-Supplies/%E7%B6%BA%E9%BA%97%E5%AF%B5%E7%89%A9%E4%B8%96%E7%95%8C-187423161294780/', lat: 24.7472685, lng: 121.7628116 },
            { 'type': 4, 'name': '小狗窩寵物美容精品', 'web': 'https://www.facebook.com/smallkennel/', lat: 24.7486326, lng: 121.7552371 },
            { 'type': 4, 'name': '國立陽明大學附設醫院 蘭陽院區', 'web': 'https://www.ymuh.ym.edu.tw/tw/', lat: 24.7489283, lng: 121.7584181 },
            { 'type': 5, 'name': '財團法人蘭陽仁愛醫院', 'web': 'http://www.jen-ai.org.tw/', lat: 24.7455641, lng: 121.7564387 },
            { 'type': 5, 'name': '維康醫療用品', 'web': 'http://www.wellcare.com.tw/', lat: 24.7485641, lng: 121.7644387 },
            { 'type': 5, 'name': '國立陽明大學附設醫院 蘭陽院區', 'web': 'https://www.ymuh.ym.edu.tw/tw/', lat: 24.7489283, lng: 121.7584181 },
            { 'type': 6, 'name': '幾米公園', 'web': 'http://okgo.tw/butyview.html?id=2897', lat: 24.7510284, lng: 121.7581902 },
            { 'type': 6, 'name': '駿懷舊餐廳', 'web': 'http://www.eland-chun.com.tw/', lat: 24.7464243, lng: 121.7569585 },
            { 'type': 6, 'name': '幾米星空車站', 'web': 'http://okgo.tw/butyview.html?id=2897', lat: 24.7525268, lng: 121.7556191 },
        ];

        var buttonSet1 = "<a href=";
        var buttonSet2 = " target=\"_blank\"; ><div class=\"buttonSet\"><i class=\"fas fa-window-restore\"style=\"color: white;\"></i> 官方網站</div></a>";

        subMap.on('focus', () => { subMap.scrollWheelZoom.enable(); });
        subMap.on('blur', () => { subMap.scrollWheelZoom.disable(); });

        var items = document.querySelectorAll('#btn1,#btn2,#btn3,#btn4,#btn5,#btn6')

        for (var i = 0; i < items.length; i++) {
            items[i].addEventListener("click", function () {
                $(this).addClass('selected').siblings().removeClass('selected')
                $(".leaflet-marker-icon").remove(); $(".leaflet-popup").remove();
                var button_Num = this.id.substring(3);
                Data.forEach(e => {
                    if (e.type == button_Num) {
                        L.marker([24.746583, 121.763135], { icon: LLWPIcon }).addTo(subMap)
                        L.marker([24.746583, 121.763135], { icon: LLWPIcon }).addTo(main_Map)
                        L.marker([e.lat, e.lng], { icon: greenIcon }).bindPopup(e.name + "<br>" + buttonSet1 + e.web + buttonSet2, { closeOnClick: false, autoClose: false }).addTo(subMap)
                        newPopUp = new L.Popup();
                        newPopUp.setLatLng(new L.LatLng(e.lat + 0.0005, e.lng + 0.00001));
                        newPopUp.setContent(e.name + "<br>" + buttonSet1 + e.web + buttonSet2);
                        subMap.addLayer(newPopUp);
                    }
                });
            })
        }

        Data.forEach(e => {
            if (e.type == "1") {
                L.marker([e.lat, e.lng], { icon: greenIcon }).bindPopup(e.name + "<br>" + buttonSet1 + e.web + buttonSet2, { closeOnClick: false, autoClose: false }).addTo(subMap)
                newPopUp = new L.Popup();
                newPopUp.setLatLng(new L.LatLng(e.lat + 0.0005, e.lng + 0.00001));
                newPopUp.setContent(e.name + "<br>" + buttonSet1 + e.web + buttonSet2);
                subMap.addLayer(newPopUp);
            }
        });

        subMap.scrollWheelZoom.disable();
        subMap.on('focus', () => { subMap.scrollWheelZoom.enable(); });
        subMap.on('blur', () => { subMap.scrollWheelZoom.disable(); });