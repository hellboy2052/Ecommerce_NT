import axios from "axios";
axios.defaults.baseURL = "https://localhost:44309";

const Product = {
    getAllProducts: async () => await axios.get("/api/Product"),
    
}

export default { Product};