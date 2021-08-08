import axios from 'axios'
import {APIURL,GetSurveyListEP} from '../lib/endpoints'

  const microCors = require('micro-cors')
const cors = microCors({ allowMethods: ['GET', 'POST', 'OPTIONS'] })

  const handler = async (req:any,res:any) => {
    res.statusCode = 200
  
  
    res.setHeader('Content-Type', 'application/json');
    res.setHeader("Strict-Transport-Security", "max-age=28800");
    res.setHeader("X-XSS-Protection", 1);
    res.setHeader("Content-Security-Policy", "default-src 'self'");
  
    

    let resp: any = await axios.get(`${APIURL}${GetSurveyListEP}`);
  console.log(req);
  
    res.end(JSON.stringify({ data: resp.data }));
  }
  
  export default cors(handler);

