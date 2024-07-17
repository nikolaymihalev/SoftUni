function solve(percents) {
    const maxPer = 100;

    if(percents == maxPer){
        console.log("100% Complete!");
        console.log("[%%%%%%%%%%]");
        return;
    }

    const maxSymbols = 10;
    const percentSymbolCount = parseInt(maxPer * (percents * 0.001));
    const dotSymbolCount = maxSymbols - percentSymbolCount;

    console.log(`${percents}% [${"%".repeat(percentSymbolCount)}${".".repeat(dotSymbolCount)}]`);
    console.log("Still loading...");
}