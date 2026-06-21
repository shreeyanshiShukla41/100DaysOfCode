import { useState } from "react";

const Ramayan=[
    {kand:"Balkand"},
    {kand:"Ayodhyakand"},
    {kand:"Kishkindhakand"},
    {kand:"Aranyakand"},
    {kand:"Sundarkand"},
    {kand:"Lankakand"},
    {kand:"Uttarkand"}
  ]

export default function Timelines(){

  const [activeKand, setActiveKand] = useState("Balkand");
  return <>
   <div className="p-8 bg-slate-900 text-white min-h-screen flex flex-col gap-4">
     <div className="flex flex-wrap gap-3">
      {Ramayan.map((e, index) => {
       return (
        <button
         key={index}
         onClick={() => setActiveKand(e.kand)}
         className={`rounded px-4 py-2 ${
          activeKand === e.kand ? "bg-orange-500" : "bg-slate-700"
         }`}
        >
         {e.kand}
        </button>
       );
      })}
     </div>

     <h2 className="text-2xl font-bold">{activeKand}</h2>
    </div>
  </>
}
