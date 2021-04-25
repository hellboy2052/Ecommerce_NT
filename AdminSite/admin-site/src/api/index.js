import axios from "axios";
//axios.defaults.baseURL = "https://localhost:44309";
axios.defaults.baseURL = "https://localhost:5001";


const Product = {
    
    getAllProducts: async () => await axios.get("/api/Product"),
    createProduct:async(data)=>await axios.post(`api/Product`,data)
}

export default { Product};