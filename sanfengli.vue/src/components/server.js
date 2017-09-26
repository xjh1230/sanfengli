import axios from 'axios';
import local from './local';

// let base = 'http://192.168.3.126:8002';
// let base = 'http://localhost:29235/';
let base = 'http://sanfengli.koalajoy.com/mvc';

/**
 * 在向服务器发送请求前对接口url进行处理
 * 可在此拼接向需要额外向服务器发送的数据
 *
 * @param {String} path 请求接口的相对地址
 */
let apiUrlHandler = path => {
    let extra = {
        async: true,
        token: local.getItem('token')
    };

    let url = `${base}${path}?`;
    let params = [];
    for (let key in extra) {
        params.push(`${key}=${extra[key]}`);
    }

    return url + params.join('&');
};





export default {
    base,

    /**
     * 以post请求的方式从指定接口获取数据
     *
     * @param {String} path 接口相对地址
     * @param {Object} params 调用接口所需要的参数
     * @param {Vue} $vue 调用接口的Vue组件对象
     * @param {Object} config axios配置信息
     */
    post: (path, params, $vue, config) => axios.post(apiUrlHandler(path), params, config || {})
        // .then(res => filters(res.data, $vue))
        // .then(res => filters(res, $vue))
        .then(res => res.data)
        .catch(err => console.info(err))
};