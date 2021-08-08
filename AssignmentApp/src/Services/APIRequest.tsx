import { APIURL } from "../lib/endpoints";

const FetchAPI = async (endpoint:string) => {
    var url = `${APIURL}${endpoint}`;
  
    
  
    const response = await fetch(url, {
      method: "GET",
      headers: { "Content-Type": "application/json " },
    });
  
    const result = await response.json();
    return result;
  };
  
  const PushApi = async (endpoint:string, payload:object) => {
    var url = `${APIURL}${endpoint}`;
  
    //url = `http://localhost:64460/employer/${endpoint}`;
  
    const response = await fetch(url, {
      method: "POST",
      headers: { "Content-Type": "application/json " },
      body: JSON.stringify(payload),
    });
  
    //console.log(url, response, payload);
    const result = await response.json();
  
    return result;
  };

  
  export {
    FetchAPI,
    PushApi,

  };
  