
import './App.css';
import React,{useState,useEffect} from 'react';
import Axios from 'axios';


export default function App() {
  const [res1,setRes]=useState([]);
  useEffect(async()=>{
    await Axios.get('https://localhost:44309/api/Category').then((res) => res.data)
    .then((res)=>setRes(
      res))
    },[])
  console.log(res1);
  return (
    <div className="App">
      <ul>
      
      </ul>
    </div>
  );

}

