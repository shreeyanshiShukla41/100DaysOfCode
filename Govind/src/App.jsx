import Timelines from './components/Timelines'

function App() {
  return (
    // Main page layout container (Full screen, dark slate background)
    <div className="min-h-screen bg-slate-950 text-white flex flex-col">
      
      {/* 1. HEADER BOX */}
      <header className="w-full px-6 py-4 bg-slate-900 border-b border-slate-800 flex justify-between items-center">
        <div className="text-xl font-bold text-amber-500">Govind</div>
        <div className="flex gap-4">
          <button className="text-sm hover:text-amber-400">Home</button>
          <button className="text-sm hover:text-amber-400">Ramayan</button>
          <button className="text-sm hover:text-amber-400">Mahabharat</button>
        </div>
      </header>

      {/* 2. CAROUSEL BOX (Placeholder height for now) */}
      <section className="w-full h-64 md:h-96 bg-slate-900/50 flex items-center justify-center border-b border-slate-800">
        <p className="text-slate-500 italic">[ Interactive Image Carousel Will Go Here ]</p>
      </section>

      {/* 3. TIMELINE BOX */}
      <main className="flex-1 p-6">
        {/* Yahan hum aapka Timeline component render karenge bad mein */}
        <p className="text-center text-amber-500 font-semibold mb-4">Timeline Section</p>
      </main>

    </div>
  );
}
export default App
