'use strict';
//var http = require('http');
var port = process.env.PORT || 8080;

//http.createServer(function (req, res) {
//    res.writeHead(200, { 'Content-Type': 'text/plain' });
//    res.end('Hello World\n');
//}).listen(port);

const express = require('express');
const cors = require('cors');
const app = express();
const bodyParser = require('body-parser');

app.use(bodyParser.json());
app.use(bodyParser.text());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.raw());

app.use(cors());

const main = () => {
    app.all('/*', getInfos);

    app.listen(port, () => {
        console.log('Listening on port ' + port);
    });
};

const getInfos = (req, res) => {
    const remoteIp = req.headers['x-forwarded-for'] || req.connection.remoteAddress;

    const infos = {
        remoteIp: remoteIp,
        method: req.method,
        url: req.url,
        _parsedUrl: req._parsedUrl,
        query: req.query,
        params: req.params,
        headers: req.headers,
        body: req.body,
        environment: process.env
    };

    console.log(infos);

    res.send(infos);
};

main();