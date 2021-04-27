import axios from "axios";
axios.defaults.baseURL = "https://localhost:44309";


const Product = {
    
    getAllProducts: async () => await axios.get("/api/Product"),
    createProduct:async(data)=>await axios.post(`api/Product`,data)
}

const Category={
    getAllCategory:async()=>await axios.get("/api/Category")
}

const Images={
    postImages:async(data)=>await axios.post("/api/Image",data)
}

export default { Product,Category,Images};